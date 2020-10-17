using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;

namespace ScoreCardTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScoreCard card1 = new ScoreCard((decimal)10000.00);
            card1.Load("StochRSITrader", GroupBy.day, 1325376000);
            ScoreCard card2 = new ScoreCard((decimal)10000.00);
            card2.Load("StochRSITrader", GroupBy.doubleday, 1325376000);
            ScoreCard card3 = new ScoreCard((decimal)10000.00);
            card3.Load("StochRSITrader", GroupBy.doublehour, 1325376000);
            ScoreCard card4 = new ScoreCard((decimal)10000.00);
            card4.Load("StochRSITrader", GroupBy.hour, 1325376000);
            //ScoreCard card5 = new ScoreCard((decimal)10000.00);
            //card5.Load("StochRSITrader", GroupBy.min, 1325376000);
            ScoreCard card6 = new ScoreCard((decimal)10000.00);
            card6.Load("StochRSITrader", GroupBy.week, 1325376000);
        }
    }
}
