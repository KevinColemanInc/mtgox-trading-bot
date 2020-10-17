using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class RSI : TradeIndicator
    {
        public RSI()
        {
            Id = 6;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries roc = WealthLab.Indicators.RSI.Series(_data.Close, 14);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = roc[roc.Count - 1];
            return ti;
        }
    }
}