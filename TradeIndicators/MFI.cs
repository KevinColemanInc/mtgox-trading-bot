using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class MFI : TradeIndicator
    {
        public MFI()
        {
            Id = 4;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries roc = WealthLab.Indicators.MFI.Series(_data, 14);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = roc[roc.Count - 1];
            if (double.IsNaN(ti.Value))
            {
                ti.Value = 0;
            }
            decimal val = (decimal)ti.Value;
            return ti;
        }
    }
}
