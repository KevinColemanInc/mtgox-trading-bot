using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinTrader.Library.BatchProcesses.Batch;
using BitcoinTrader.Library.Business;
using BitcoinTrader.Library.TradingAlgorithms.TradingAlgorithmBase;
using KevinColemanInc.Utilities;
using TradeBucketCache;
using LinqToTwitter;
using MtGoxTrader.MtGoxAPIClient;
using MtGoxTrader.Model;
using BitcoinTrader.Library.TradingAlgorithms.StochRSIAlgorithms;
using System.Configuration;

namespace BitcoinTrader.Library.BatchProcesses.TradeCommandProcess
{
    public class TradeCommander : BatchProcess
    {
        public TradeCommander(Cache cache, object process)
            : base(cache, process)
        {

        }
        public override void Run()
        {
            while (true)
            {
                runAlgorithm();
                Thread.Sleep(60*3*1000);
            }
        }

        private void runAlgorithm()
        {
            TradingAlgorithm tradeAlgo = (TradingAlgorithm)_process;
            List<Advice> advices = new List<Advice>();
            TradeBucket end = TradeBucket.GetTwoLastKnownBucket(tradeAlgo.Group)[0];
            double lastKnownTime = Advice.GetLastKnownTime(tradeAlgo.Name, tradeAlgo.Group);
            double start = lastKnownTime > 1293840000 ? lastKnownTime : _cache.GetByGroup(tradeAlgo.Group)[70].Time;
            bool hasCash = true;
            for (double i = start; i <= end.Time; i += (double)tradeAlgo.Group)
            {
                List<TradeBucket> tb = _cache.GetBucketsBeforeTime(tradeAlgo.Group, i);
                Advice advice = tradeAlgo.GetActionForBuckets(tb);

                if (hasCash == true && advice.Action == TradeAction.Buy)
                {
                    advices.Add(advice);
                    hasCash = false;
                }
                else if (hasCash == false && advice.Action == TradeAction.Sell)
                {
                    advices.Add(advice);
                    hasCash = true;
                }
            }

            foreach (Advice advice in advices)
            {
                Advice ad = (Advice)advice.Exists();
                if (ad == null && advice.Group == GroupBy.hour && (advice.AlgorithmName == "StochRSI" || advice.AlgorithmName == "ROC"))
                {
                    if (bool.Parse(ConfigurationManager.AppSettings["enableEmailing"]))
                    {
                        sendEmailsFor(advice);
                    }
                    if (bool.Parse(ConfigurationManager.AppSettings["enableTwitter"]))
                    {
                        tweetFor(advice);
                    }
                    if (bool.Parse(ConfigurationManager.AppSettings["enableTrading"]))
                    {
                        mtgoxTrade(advice);
                    }
                }
                advice.SaveNew();
            }
            this._doneEvent.Set();
        }
        private void mtgoxTrade(Advice advice)
        {
             if (advice.Group == GroupBy.hour)
            {
                MtGoxAPIV0 v0 = new MtGoxAPIV0();
                v0.apiKey = ConfigurationManager.AppSettings["mtgoxAPIKey"];
                v0.apiSecret = ConfigurationManager.AppSettings["mtgoxAPISecret"];

                List<MtGoxOrder> orders = v0.getOrders(MtGoxOrderType.Buy, MtGoxOrderStatus.Invalid);
                orders.AddRange(v0.getOrders(MtGoxOrderType.Buy, MtGoxOrderStatus.Active));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Buy, MtGoxOrderStatus.NotSufficientFunds));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Buy, MtGoxOrderStatus.Pending));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Sell, MtGoxOrderStatus.Active));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Sell, MtGoxOrderStatus.NotSufficientFunds));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Sell, MtGoxOrderStatus.Pending));
                orders.AddRange(v0.getOrders(MtGoxOrderType.Sell, MtGoxOrderStatus.Invalid));

                foreach (MtGoxOrder order in orders)
                {
                    v0.cancelOrder(order.oid, order.type);
                }

                decimal adjustedPrice = decimal.MaxValue;
                if (advice.Action == TradeAction.Buy)
                {
                    MtGoxHistoryItem funds = v0.info();
                    double cashOnHand = 0;
                    foreach(MtGoxWallet wall in funds.Wallets)
                    {
                        if (wall.name == MtGoxCurrencySymbol.USD)
                        {
                            cashOnHand = wall.balance.value;
                        }
                    }
                    adjustedPrice = advice.Price * (decimal)(1 - funds.Trade_Fee);
                    v0.buyBTC(cashOnHand, MtGoxCurrencySymbol.USD, (double)adjustedPrice);
                }
                else if (advice.Action == TradeAction.Sell)
                {
                    MtGoxHistoryItem funds = v0.info();
                    double cashOnHand = 0;
                    foreach (MtGoxWallet wall in funds.Wallets)
                    {
                        if (wall.name == MtGoxCurrencySymbol.BTC)
                        {
                            cashOnHand = wall.balance.value;
                        }
                    }
                    adjustedPrice = advice.Price * (decimal)(1 + funds.Trade_Fee);
                    v0.sellBTC(21, MtGoxCurrencySymbol.USD, (double)adjustedPrice);
                }
            }
        }
        private void sendEmailsFor(Advice advice)
        {
            List<string> emails = Subscription.FetchUserEmailsForAdvice(advice);

            string subject = string.Empty;
            string body = string.Empty;

            subject += string.Format("BC.Trader {0}, {1}  Alert", advice.AlgorithmName, advice.Group.ToString());
            body += string.Format("{5}--${0:N4}--{1}--{2}--{3}--{4}",
                advice.Price, advice.DTime, advice.Group.ToString(), advice.AlgorithmName, advice.Confidence, advice.Action.ToString());
            foreach (string email in emails)
            {
                MailUtil.SendEmail(email, subject, body);
            }
            advice.EmailSent = true;
        }
        private void tweetFor(Advice advice)
        {
            var auth = new SingleUserAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"],
                    OAuthToken = ConfigurationManager.AppSettings["twitterOAuthToken"],
                    AccessToken = ConfigurationManager.AppSettings["twitterAccessToekn"]
                }
            };
            using (var twitterCtx = new TwitterContext(auth))
            {
                string status = advice.ToString();
                var tweet = twitterCtx.UpdateStatus(status.Substring(0, status.Length > 140 ? 140 : status.Length));
            }

        }
    }
}