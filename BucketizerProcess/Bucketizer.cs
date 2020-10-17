using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinTrader.Library.BatchProcesses.Batch;
using BitcoinTrader.Library.Business;
using TradeBucketCache;

namespace BitcoinTrader.Library.BucketizerProcess
{
    public class Bucketizer : BatchProcess
    {
        public Bucketizer(Cache cache, object process)
            : base(cache, process)
        {

        }

        public override void Run()
        {
            //Fetch data From DB
            while (true)
            {
                List<TradeBucket> lastBucket = TradeBucket.GetTwoLastKnownBucket((GroupBy)_process);
                List<Trade> remainingTrades = Trade.GetTradesAfterTime(lastBucket.Count == 0 ? 0 : lastBucket[1].Time);
                //Bucket the data
                List<TradeBucket> buckets = groupDataBy((GroupBy)_process, remainingTrades, lastBucket.Count == 0 ? null : lastBucket[0]);
                //Save Buckets

                saveAllBuckets(buckets);
                Thread.Sleep(60 * 3 * 1000);
            }
        }
        private void saveAllBuckets(List<TradeBucket> bucketstoSave)
        {
            foreach (TradeBucket bucket in bucketstoSave)
            {
                if (bucket.NumTrades > 0)
                {
                    List<TradeBucket> tb = _cache.GetByGroup(bucket.Group);
                    if (tb[tb.Count - 1].Time == bucket.Time)
                    {
                        tb[tb.Count - 1] = bucket;
                    }
                    else
                    {
                        _cache.AddBucket(bucket);
                    }

                    bucket.SaveNew();
                }
            }
        }
        private static List<TradeBucket> groupDataBy(GroupBy time, List<Trade> trades, TradeBucket lastBucket)
        {
            if (time == GroupBy.hour)
            {
                int x = 0;
            }
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
                        bucket.Close = trades[i - 1].Price;
                        bucket.NumTrades = cNumTrades;
                        bucket.Group = time;
                        grouped.Add(bucket);

                        cBucketTime += (double)time;
                        cNumTrades = 0;
                        cHigh = decimal.MinValue;
                        cLow = decimal.MaxValue;
                        cVolume = 0;
                    }

                    cOpen = grouped.Count == 0 ? lastBucket != null ? lastBucket.Close : trades[0].Price : grouped[grouped.Count - 1].Close;
                    cNumTrades++;
                    cHigh = trades[i].Price > cHigh ? trades[i].Price : cHigh;
                    cLow = trades[i].Price < cLow ? trades[i].Price : cLow;
                    cVolume += trades[i].Amount;
                }
            }
            grouped[0].Open = lastBucket == null ? trades[0].Price : lastBucket.Close;
            return grouped;
        }

    }
}
