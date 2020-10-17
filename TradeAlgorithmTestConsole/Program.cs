using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.BatchProcesses.TradeCommandProcess;
using BitcoinTrader.Library.Business;
using BitcoinTrader.Library.TradingAlgorithms.StochRSIAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.TradingAlgorithmBase;

namespace TradeAlgorithmTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TradeCommander comm1 = new TradeCommander(new SimpleStochRSI(GroupBy.day));
            comm1.Run();
            TradeCommander comm2 = new TradeCommander(new SimpleStochRSI(GroupBy.doubleday));
            comm2.Run();
            TradeCommander comm3 = new TradeCommander(new SimpleStochRSI(GroupBy.doublehour));
            comm3.Run();
            TradeCommander comm4 = new TradeCommander(new SimpleStochRSI(GroupBy.hour));
            comm4.Run();
            TradeCommander comm5 = new TradeCommander(new SimpleStochRSI(GroupBy.week));
            comm5.Run();
            TradeCommander comm6 = new TradeCommander(new SimpleStochRSI(GroupBy.halfhour));
            comm6.Run();
        }        
    }
}
