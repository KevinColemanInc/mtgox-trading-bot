using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class ROC : TradeIndicator
    {
        public ROC()
        {
            Id = 5;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries roc = WealthLab.Indicators.ROC.Series(_data.Close, 20);
            DataSeries SMARoc = WealthLab.Indicators.SMA.Series(roc, 14);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = SMARoc[SMARoc.Count - 1];
            return ti;
        }
    }
}
