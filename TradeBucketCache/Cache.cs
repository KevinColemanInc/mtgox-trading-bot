using BitcoinTrader.Library.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeBucketCache
{
    public class Cache
    {
        private static Dictionary<GroupBy, List<TradeBucket>> tbCache = new Dictionary<GroupBy, List<TradeBucket>>();

        public Cache()
        {
            foreach (GroupBy foo in Enum.GetValues(typeof(GroupBy)))
            {
                tbCache.Add(foo, TradeBucket.GetAllBuckets(foo));
            }
        }
        public List<TradeBucket> GetByGroup(GroupBy group)
        {
            return tbCache[group];
        }
        public void AddBucket(TradeBucket tb)
        {
            tbCache[tb.Group].Add(tb);
        }
        public List<TradeBucket> GetBucketsBeforeTime(GroupBy group, double time)
        {
            List<TradeBucket> tb = tbCache[group];

            for (int end = 0; end < tb.Count; end++)
            {
                if (tb[end].Time == time)
                {
                    return tb.GetRange(end - 64, 65);
                }
            }
            return null;
        }

    }
}
