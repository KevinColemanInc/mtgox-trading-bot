using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;
using BitcoinTrader.Library.TradeIndicators.TradeIndicators;
using BitcoinTrader.Library.TradingAlgorithms.TradingAlgorithmBase;

namespace BitcoinTrader.Library.TradingAlgorithms.CSIAlgorithms
{
    public class SimpleCCI : TradingAlgorithm
    {
        public SimpleCCI(GroupBy group)
            : base(group)
        {
            Name = "CCI";
        }

        public override Advice GetActionForBuckets(List<TradeBucket> tradeBuckets)
        {
            Advice advice = new Advice();
            advice.AlgorithmName = Name;
            advice.Confidence = 0;
            advice.Group = Group;
            advice.Time = tradeBuckets[tradeBuckets.Count-1].Time;

            List<TradeBucket> buckets = tradeBuckets;
            CCI rsi = new CCI();

            TIOutput output = rsi.GetCurrentValue(buckets);

            advice.Price = buckets[buckets.Count - 1].Close;
            advice.Confidence = (decimal)output.Value;

            if (output.Value > 190)
            {
                advice.Action = TradeAction.Sell;
            }
            else if (output.Value < -200)
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
