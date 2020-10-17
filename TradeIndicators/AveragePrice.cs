using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class AveragePrice : TradeIndicator
    {

        public AveragePrice()
        {
            Id = 1;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries avg = WealthLab.Indicators.AveragePrice.Series(_data);

            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = avg[avg.Count - 1];
            return ti;
        }
    }
}
