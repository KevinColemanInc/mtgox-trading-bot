using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;
using BitcoinTrader.Library.TradeIndicators.TradeIndicators;
using BitcoinTrader.Library.TradingAlgorithms.TradingAlgorithmBase;

namespace BitcoinTrader.Library.TradingAlgorithms.WilliamsRAlgorithms
{
    public class SimpleWilliamsR : TradingAlgorithm
    {
        public SimpleWilliamsR(Business.GroupBy group)
            : base(group)
        {
            Name = "WilliamsR";
        }

        public override Advice GetActionForBuckets(List<TradeBucket> tradeBuckets)
        {
            Advice advice = new Advice();
            advice.AlgorithmName = Name;
            advice.Confidence = 0;
            advice.Group = Group;
            advice.Time = tradeBuckets[tradeBuckets.Count - 1].Time;

            List<TradeBucket> buckets = tradeBuckets;
            WilliamsR rsi = new WilliamsR();

            TIOutput output = rsi.GetCurrentValue(buckets);

            advice.Price = buckets[buckets.Count - 1].Close;
            advice.Confidence = (decimal)output.Value;

            if (output.Value < 20)
            {
                advice.Action = TradeAction.Sell;
            }
            else if (output.Value > 80)
            {
                advice.Action = TradeAction.Buy;
            }
            else
            {
                advice.Action = TradeAction.Hold;
            }

            return advice;
        }
    }
}
