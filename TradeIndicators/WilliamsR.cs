using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class WilliamsR : TradeIndicator
    {
        public WilliamsR()
        {
            Id = 9;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries PctR = WealthLab.Indicators.WilliamsR.Series(_data, 14);
            DataSeries SmoothR = WealthLab.Indicators.WilderMA.Series(PctR, 4);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = SmoothR[SmoothR.Count - 1];
            return ti;

        }
    }
}
