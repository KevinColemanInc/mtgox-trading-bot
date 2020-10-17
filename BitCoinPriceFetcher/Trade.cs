using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KevinColemanInc.Utilities;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;

namespace BitcoinTrader.Library.Business
{
    public class Trade : DataObject
    {
        public double Time = 0;
        public decimal Price = 0;
        public decimal Amount = 0;
        public long Tid;
        public string Currency;
        public string TradeType;

        private static List<Trade> tradeCache = new List<Trade>();
        private static DateTime cacheAge = DateTime.MinValue;

        public DateTime DTime
        {
            get
            {
                return TimeUtil.FromUnixTime((long)Time);
            }
            set
            {
                Time = (int)TimeUtil.ToUnixTime(value);
            }
        }

        public Trade()
        {

        }
        public Trade(int time, decimal price, decimal amount)
        {
            Time = time;
            Price = price;
            Amount = amount;
        }

        public static List<Trade> ParseTrades(string rawMtGoxTrades)
        {
            List<Trade> trades = new List<Trade>();
            
                string[] rows = rawMtGoxTrades.Split('{');
                for (int i = 2; i < rows.Length; i++)
                {
                    string[] tradeProperties = rows[i].Split(',');

                    Trade currentTrade = new Trade();
                    currentTrade.Time = int.Parse(tradeProperties[0].Split(':')[1]);
                    currentTrade.Amount = decimal.Parse(tradeProperties[2].Split(':')[1].Trim('\"'), System.Globalization.NumberStyles.Float);
                    currentTrade.Price = decimal.Parse(tradeProperties[3].Split(':')[1].Trim('\"')) / 100000;
                    currentTrade.Tid = long.Parse(tradeProperties[5].Split(':')[1].Trim('\"'));
                    currentTrade.Currency = tradeProperties[6].Split(':')[1].Trim('\"');
                    currentTrade.TradeType = tradeProperties[8].Split(':')[1].Trim('\"');
                    trades.Add(currentTrade);
                    
                }
            

            return trades;
        }

        public static long GetLastKnownTid()
        {
            long last = 0;
            string query = string.Format(@"SELECT Max(tid) FROM raw_trades");
            DataTable dt = RunSELECT(query);

            if (dt.Rows[0][0].ToString() != string.Empty)
                last = long.Parse(dt.Rows[0][0].ToString());

            return last;
        }

        public static int GetLastKnownTime()
        {
                DateTime last = DateTime.MinValue;

                string query = string.Format(@"SELECT Max(time) FROM trades");
                DataTable dt = RunSELECT(query);

                if (dt.Rows.Count > 0)
                    last = (DateTime)dt.Rows[0][0];

                if (last == DateTime.MinValue)
                    return 0;
                return (int)TimeUtil.ToUnixTime(last);
        }

        public static List<Trade> GetTradesAfterTime(double time)
        {
            List<Trade> trades = new List<Trade>();

            string query = string.Format(@"SELECT Time, Price, Amount FROM raw_trades where Time > '{0}' Order By Time Asc limit 10000", TimeUtil.FromUnixTime((long)time).ToString("yyyy-MM-dd HH:mm:ss"));
                DataTable dt = RunSELECT(query);

                foreach (DataRow dr in dt.Rows)
                {
                    Trade trade = new Trade();
                    trade.Time = (int)TimeUtil.ToUnixTime((DateTime)dr[0]);
                    trade.Price = (decimal)dr[1];
                    trade.Amount = (decimal)dr[2];
                    trades.Add(trade);
                }
                return trades;
        }

        public override DataObject Exists()
        {
            return null;
        }

        public override void Update(DataObject dataobject)
        {
            //just skip over it.  we will never have to update trades;
            return;
        }

        public override void SQLSave()
        {
            string sqlIns = "INSERT INTO raw_trades (tid, Time, Price, Amount, trade_type, currency, Created_at, updated_at) VALUES (@Tid, @Time, @Price, @Amount, @trade_type, @currency, @Created_at, @updated_at)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStringLocal))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sqlIns;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Tid", Tid);
                    cmd.Parameters.AddWithValue("@Time", TimeUtil.FromUnixTime((long)Time));
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@currency", Currency);
                    cmd.Parameters.AddWithValue("@trade_type", TradeType);
                    cmd.Parameters.AddWithValue("@Created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@updated_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
        public string ToCSV()
        {
            return string.Format("{0}\t{1}\t{2}", DTime.ToString("yyyy-MM-dd HH:mm:ss.000"), Price, Amount);
        }
        public override string ToJSON()
        {
            return "{" + string.Format("\"time\":\"{0}\",\"price\":\"{1}\",\"amount\":\"{2}\"", DTime.ToString("yyyy-MM-dd HH:mm:ss.000"), Price, Amount) + "}";
        }
    }
}
