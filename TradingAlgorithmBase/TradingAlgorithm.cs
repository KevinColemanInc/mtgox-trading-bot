using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.TradingAlgorithms.TradingAlgorithmBase
{
    public abstract class TradingAlgorithm
    {
        public String Name;
        public GroupBy Group;
        public TradingAlgorithm(GroupBy group)
        {
            Group = group;
        }
        /// <summary>
        /// Whether or not you should buy
        /// </summary>
        /// <returns>amount to buy</returns>
        abstract public Advice GetActionForBuckets(List<TradeBucket> tradeBuckets);
        
    }
}
