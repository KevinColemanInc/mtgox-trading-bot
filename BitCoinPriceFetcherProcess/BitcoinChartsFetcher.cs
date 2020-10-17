using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;

namespace BitcoinTrader.Library.BatchProcesses.BitCoinPriceFetcher
{
    public class BitcoinChartsFetcher : Batch.BatchProcess
    {
        public BitcoinChartsFetcher(object process)
            : base(null, process)
        {
        }

        public override void Run()
        {
            while (true)
            {
                int last = getLatestKnownTrade();

                fetchAndSaveLatestTrades(last);

                Thread.Sleep(16 * 1000 * 60);
            }
        }

        private int getLatestKnownTrade()
        {
            return Trade.GetLastKnownTime();
        }

        private void fetchAndSaveLatestTrades(int time)
        {
            try
            {
                StringBuilder rawData = KevinColemanInc.Utilities.WebUtil.HTTPGet(string.Format("http://bitcoincharts.com/t/trades.csv?symbol=mtgoxUSD&start={0}", time));
            
                string[] rawTrades = rawData.ToString().Split('\n');
                foreach (string rawTrade in rawTrades)
                {
                    string[] splitTrades = rawTrade.Split(',');

                    Trade trade = new Trade(int.Parse(splitTrades[0]), decimal.Parse(splitTrades[1]), decimal.Parse(splitTrades[2]));
                    trade.SaveNew();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }

    }
}
