using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.Business
{

    public class AlgorithmTrade
    {
        public decimal PurchasePrice;
        public decimal SellPrice;
        public double SellTime;

        public decimal PercentChange
        {
            get
            {
                return (SellPrice - PurchasePrice) / PurchasePrice;
            }
        }
    }
}
