using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdviceAlertProcess;
using BitcoinTrader.Library.BatchProcesses.Batch;
using BitcoinTrader.Library.BatchProcesses.BitCoinPriceFetcher;
using BitcoinTrader.Library.BatchProcesses.TradeCommandProcess;
using BitcoinTrader.Library.BucketizerProcess;
using BitcoinTrader.Library.Business;
using BitcoinTrader.Library.TradingAlgorithms.CSIAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.DPOAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.MFIAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.MomentumAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.StochRSIAlgorithms;
using BitcoinTrader.Library.TradingAlgorithms.WilliamsRAlgorithms;
using TradeBucketCache;
using System.Configuration;
using BitcoinTrader.Library.TradingAlgorithms.ROCAlgorithms;

namespace BatchProcessManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Batch Process Manager Console";
            Console.WriteLine("Run-Time Settings\n");
            Console.WriteLine("enable trading: {0}", ConfigurationManager.AppSettings["enableTrading"]);
            Console.WriteLine("enable RDS: {0}", ConfigurationManager.AppSettings["enableRDS"]);
            Console.WriteLine("enable Twitter: {0}", ConfigurationManager.AppSettings["enableTwitter"]);
            Console.WriteLine("enable Emailing: {0}", ConfigurationManager.AppSettings["enableEmailing"]);
            Console.WriteLine("");
            DateTime startTime = DateTime.Now;
            Console.WriteLine("started at {0}", startTime);
            Cache cache = new Cache();

            BatchProcessManager manager = new BatchProcessManager();

            //1 Adds raw_trades to database
            manager.AddBatch(new MtGoxChartsFetcher(null));
         
            foreach (GroupBy foo in Enum.GetValues(typeof(GroupBy)))
            {
                
                //2 groups the data
                manager.AddBatch(new Bucketizer(cache, foo));
                
                //3 Algorithms
                manager.AddBatch(new TradeCommander(cache, new SimpleStochRSI(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleROC(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleMFI(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleCCI(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleDPO(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleMomentum(foo)));

                manager.AddBatch(new TradeCommander(cache, new SimpleWilliamsR(foo)));
            }


            manager.Start();
            while (true)
            {
                Console.WriteLine("{0}", DateTime.Now - startTime);
                Thread.Sleep(1000 * 60 * 60);
            }
        }
    }
}
