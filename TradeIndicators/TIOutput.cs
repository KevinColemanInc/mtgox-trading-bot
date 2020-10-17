using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitcoinTrader.Library.TradeIndicators.TradeIndicators
{
    public class TIOutput
    {
        public double Time;
        public int Id;
        public double Value;
        public string ToJSON()
        {
            return "{" + string.Format("\"trade_indicator_id\":{0}, \"time\":{1}, \"value\":{2}", Id, Time, Value) + "}";
        }
        public string ToCSV()
        {
            return string.Format("{0},{1},{2}", Id, Time, Value);
        }
    }
}
