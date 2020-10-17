using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using WealthLab.Indicators;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
   public class StochRSI : TradeIndicator
    {
       public StochRSI()
        {
            Id = 8;
        }
       public override TIOutput GetCurrentValue(List<TradeBucket> buckets)
        {
            BucketsToBars(buckets);
            DataSeries stoRSI = WealthLab.Indicators.StochRSI.Series(_data.Close, 14);
            TIOutput ti = new TIOutput();
            ti.Id = this.Id;
            ti.Time = buckets[buckets.Count - 1].Time;
            ti.Value = stoRSI[stoRSI.Count - 1];
            return ti;
        }
    }
}
