using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WealthLab;
using BitcoinTrader.Library.Business;
using KevinColemanInc.Utilities;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public abstract class TradeIndicator
    {
        
        public int Id;

        protected List<TradeBucket> _buckets;
        protected Bars _data = null;

        public TradeIndicator() : base()
        {
           
        }

        abstract public TIOutput GetCurrentValue(List<TradeBucket> buckets);

        /// <summary>
        /// Sets up the bars object.  Must be called with every new set of data.
        /// </summary>
        /// <param name="buckets">buckets BCCharts</param>
        protected void BucketsToBars(List<TradeBucket> buckets)
        {
            _data = new Bars("SADF", BarScale.Minute, 60);
            _buckets = buckets;

            foreach (TradeBucket bucket in buckets)
                _data.Add(TimeUtil.FromUnixTime((long)bucket.Time), (double)bucket.Open, (double)bucket.High, (double)bucket.Low, (double)bucket.Close, (double)bucket.Volume);

        }
    }
}
