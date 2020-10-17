using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;

namespace TableToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            int lastTradeTime = Trade.GetLastKnownTime();

            StringBuilder rawData = KevinColemanInc.Utilities.WebUtil.HTTPGet(string.Format("http://bitcoincharts.com/t/trades.csv?symbol=mtgoxUSD&start={0}", lastTradeTime));

            string[] rawTrades = rawData.ToString().Split('\n');
            List<String> tradeCSV = new List<String>();
            
            foreach (string rawTrade in rawTrades)
            {
                string[] splitTrades = rawTrade.Split(',');
                Trade trade = new Trade(int.Parse(splitTrades[0]), decimal.Parse(splitTrades[1]), decimal.Parse(splitTrades[2]));
                tradeCSV.Add(trade.ToCSV());
            }

            File.WriteAllLines("C:\\Users\\kcoleman\\Dropbox\\raw_trades.csv", tradeCSV.ToArray());
        }
    }
}
