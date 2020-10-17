using BitcoinTrader.Library.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.BatchProcesses.BitCoinPriceFetcher
{
    public class MtGoxChartsFetcher: Batch.BatchProcess
    {
        public MtGoxChartsFetcher(object process)
            : base(null, process)
        {
        }

        public override void Run()
        {
            while (true)
            {
                long last = getLatestKnownTrade();

                fetchAndSaveLatestTrades(last);

                Thread.Sleep(3 * 60 * 1000);
            }
        }

        private long getLatestKnownTrade()
        {
            return Trade.GetLastKnownTid();
        }

        private void fetchAndSaveLatestTrades(long tid)
        {
              StringBuilder rawData = KevinColemanInc.Utilities.WebUtil.HTTPGet(string.Format("https://mtgox.com/api/1/BTCUSD/trades?since={0}", tid));

                List<Trade> trades = Trade.ParseTrades(rawData.ToString());

                foreach(Trade trade in trades)
                {
                    trade.SaveNew();
                }
        }

    }
}
