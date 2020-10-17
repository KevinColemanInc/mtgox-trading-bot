using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.BucketizerProcess;
using BitcoinTrader.Library.Business;
using KevinColemanInc.Utilities;

namespace BucketizerTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                double time = TimeUtil.ToUnixTime(new DateTime(2011,06,1));
                List<Trade> remainingTrades = Trade.GetTradesAfterTime(time);
                //Bucket the data
                List<TradeBucket> buckets = groupDataBy(GroupBy.day, remainingTrades);
            }

        }
        private static List<TradeBucket> groupDataBy(GroupBy time, List<Trade> trades)
        {
            List<TradeBucket> grouped = new List<TradeBucket>();
            if (trades.Count > 0)
            {
                double cBucketTime = (trades[0].Time - (trades[0].Time % (int)time));

                decimal cHigh = decimal.MinValue;
                decimal cLow = decimal.MaxValue;
                decimal cVolume = 0;
                int cNumTrades = 0;
                decimal cOpen = (decimal)0;

                int tradeCount = 0;

                for (int i = 0; i < trades.Count; i++)
                {
                    tradeCount++;

                    if (trades[i].Time > (cBucketTime + (int)time) || tradeCount == trades.Count)
                    {
                        TradeBucket bucket = new TradeBucket();
                        bucket.Time = cBucketTime;
                        bucket.High = cHigh;
                        bucket.Low = cLow;
                        bucket.Volume = cVolume;
                        bucket.Open = cOpen;
                        bucket.Close = trades[i - 1].Price
                            ;
                        bucket.NumTrades = cNumTrades;
                        bucket.Group = time;
                        grouped.Add(bucket);

                        cBucketTime += (double)time;
                        cNumTrades = 0;
                        cHigh = decimal.MinValue;
                        cLow = decimal.MaxValue;
                        cVolume = 0;
                    }

                    cOpen = cVolume == 0 ? trades[i].Price : cOpen;
                    cNumTrades++;
                    cHigh = trades[i].Price > cHigh ? trades[i].Price : cHigh;
                    cLow = trades[i].Price < cLow ? trades[i].Price : cLow;
                    cVolume += trades[i].Amount;

                }
            }
            return grouped;
        }
    }
}
