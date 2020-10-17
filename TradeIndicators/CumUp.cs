using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class CumUp : TradeIndicator
    {
        public CumUp()
        {
            Id = 3;
        }
        public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = Math.Truncate(WealthLab.Indicators.CumUp.Series(_data.Close, 3)[_data.Count - 1]);
            return ti;
        }
    }
}
