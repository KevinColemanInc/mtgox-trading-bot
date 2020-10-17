using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.BatchProcesses.BitCoinPriceFetcher;

namespace TestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            MtGoxChartsFetcher fetcher = new MtGoxChartsFetcher(null);
            fetcher.Run();


        }
    }
}
