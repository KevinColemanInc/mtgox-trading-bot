using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KevinColemanInc.Utilities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace BitcoinTrader.Library.Business
{
    [Serializable()]
    public class TradeBucket : DataObject
    {
        public int Id;
        public double Time;
        public decimal High;
        public decimal Low;
        public decimal Volume;
        public decimal Open;
        public decimal Close;
        public int NumTrades;
        public GroupBy Group;

        public DateTime DTime
        {
            get
            {
                return TimeUtil.FromUnixTime((long)Time);
            }
            set
            {
                Time = TimeUtil.ToUnixTime(value);
            }
        }

        public static List<TradeBucket> GetTwoLastKnownBucket(GroupBy group)
        {

            string query = string.Format(@"SELECT Time, High, Low, Volume, open, close, number_of_trades, bucket_group, ID FROM trade_buckets where bucket_group = '{0}' Order By Time desc limit 2", group.ToString());
            DataTable dt = RunSELECT(query);
            List<TradeBucket> tradeBuckets = new List<TradeBucket>();

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                TradeBucket tradeBucket = new TradeBucket();
                tradeBucket.Time = (int)TimeUtil.ToUnixTime((DateTime)dt.Rows[i][0]);
                tradeBucket.High = (decimal)dt.Rows[i][1];
                tradeBucket.Low = (decimal)dt.Rows[i][2];
                tradeBucket.Volume = (decimal)dt.Rows[i][3];
                tradeBucket.Open = (decimal)dt.Rows[i][4];
                tradeBucket.Close = (decimal)dt.Rows[i][5];
                tradeBucket.NumTrades = (int)dt.Rows[i][6];
                tradeBucket.Group = GroupUtil.GetGroupBy((string)dt.Rows[i][7]);
                tradeBucket.Id = (int)dt.Rows[i][8];
                tradeBuckets.Add(tradeBucket);

            }
            return tradeBuckets;
        }
        public static List<TradeBucket> GetAllBuckets(GroupBy group)
        {
            string query = string.Format(@"SELECT Time, High, Low, Volume, open, close, number_of_trades, bucket_group, ID FROM trade_buckets WHERE bucket_group = '{0}' Order By Time desc", group.ToString());
            DataTable dt = RunSELECT(query);

            List<TradeBucket> results = new List<TradeBucket>();
            //need to reverse order
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                TradeBucket tradeBucket = new TradeBucket();
                tradeBucket.Time = (int)TimeUtil.ToUnixTime((DateTime)dt.Rows[i][0]);
                tradeBucket.High = (decimal)dt.Rows[i][1];
                tradeBucket.Low = (decimal)dt.Rows[i][2];
                tradeBucket.Volume = (decimal)dt.Rows[i][3];
                tradeBucket.Open = (decimal)dt.Rows[i][4];
                tradeBucket.Close = (decimal)dt.Rows[i][5];
                tradeBucket.NumTrades = (int)dt.Rows[i][6];
                tradeBucket.Group = GroupUtil.GetGroupBy((string)dt.Rows[i][7]);
                tradeBucket.Id = (int)dt.Rows[i][8];
                results.Add(tradeBucket);
            }
            return results;
        }
        public static List<TradeBucket> GetLastBuckets(GroupBy group, double time, int buckets)
        {
                DateTime last = DateTime.MinValue;

                DateTime endTime = TimeUtil.FromUnixTime((long)time);

                string query = string.Format(@"SELECT Time, High, Low, Volume, open, close, number_of_trades, bucket_group, ID FROM trade_buckets WHERE bucket_group = '{0}' AND TIME <= '{1}' Order By Time desc limit {2}", group.ToString(), endTime.ToString("yyyy-MM-dd HH:mm:ss"), buckets);
                DataTable dt = RunSELECT(query);

                List<TradeBucket> results = new List<TradeBucket>();
                //need to reverse order
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    TradeBucket tradeBucket = new TradeBucket();
                    tradeBucket.Time = (int)TimeUtil.ToUnixTime((DateTime)dt.Rows[i][0]);
                    tradeBucket.High = (decimal)dt.Rows[i][1];
                    tradeBucket.Low = (decimal)dt.Rows[i][2];
                    tradeBucket.Volume = (decimal)dt.Rows[i][3];
                    tradeBucket.Open = (decimal)dt.Rows[i][4];
                    tradeBucket.Close = (decimal)dt.Rows[i][5];
                    tradeBucket.NumTrades = (int)dt.Rows[i][6];
                    tradeBucket.Group = GroupUtil.GetGroupBy((string)dt.Rows[i][7]);
                    tradeBucket.Id = (int)dt.Rows[i][8];
                    results.Add(tradeBucket);
                }
                return results;
        }

        public override DataObject Exists()
        {
            string query = string.Format(@"SELECT Time, High, Low, Volume, open, close, number_of_trades, bucket_group, ID FROM trade_buckets WHERE Time = '{0}' AND bucket_group = '{1}'", TimeUtil.FromUnixTime((long)Time).ToString("yyyy-MM-dd HH:mm:ss"), Group);
            DataTable dt = RunSELECT(query);

            if (dt.Rows.Count > 0)
            {
                TradeBucket tradeBucket = new TradeBucket();
                tradeBucket.Time = (int)TimeUtil.ToUnixTime((DateTime)dt.Rows[0][0]);
                tradeBucket.High = (decimal)dt.Rows[0][1];
                tradeBucket.Low = (decimal)dt.Rows[0][2];
                tradeBucket.Volume = (decimal)dt.Rows[0][3];
                tradeBucket.Open = (decimal)dt.Rows[0][4];
                tradeBucket.Close = (decimal)dt.Rows[0][5];
                tradeBucket.NumTrades = (int)dt.Rows[0][6];
                tradeBucket.Group = GroupUtil.GetGroupBy((string)dt.Rows[0][7]);
                tradeBucket.Id = (int)dt.Rows[0][8];
                return tradeBucket;
            }
            else
            {
                return null;
            }
        }
        public override void SQLSave()
        {
            string sqlIns = "INSERT INTO trade_buckets (Time, High, Low, Volume, open, close, number_of_trades, bucket_group, Created_at, updated_at) VALUES (@Time, @High, @Low, @Volume, @Open, @Close, @NumTrades, @GroupBy, @Created_at,@Created_at)";

            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["enableRDS"]))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionStringRDS))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sqlIns;
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@Time", TimeUtil.FromUnixTime((long)Time));
                        cmd.Parameters.AddWithValue("@High", High);
                        cmd.Parameters.AddWithValue("@Low", Low);
                        cmd.Parameters.AddWithValue("@Volume", Volume);
                        cmd.Parameters.AddWithValue("@Open", Open);
                        cmd.Parameters.AddWithValue("@Close", Close);
                        cmd.Parameters.AddWithValue("@NumTrades", NumTrades);
                        cmd.Parameters.AddWithValue("@Created_at", DateTime.Now);
                        cmd.Parameters.AddWithValue("@GroupBy", Group.ToString());
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                }
                using (MySqlConnection conn = new MySqlConnection(connectionStringLocal))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sqlIns;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Time", TimeUtil.FromUnixTime((long)Time));
                    cmd.Parameters.AddWithValue("@High", High);
                    cmd.Parameters.AddWithValue("@Low", Low);
                    cmd.Parameters.AddWithValue("@Volume", Volume);
                    cmd.Parameters.AddWithValue("@Open", Open);
                    cmd.Parameters.AddWithValue("@Close", Close);
                    cmd.Parameters.AddWithValue("@NumTrades", NumTrades);
                    cmd.Parameters.AddWithValue("@Created_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@GroupBy", Group.ToString());
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

        public override void Update(DataObject exists)
        {
            merge((TradeBucket)exists);

            string sqlIns = "UPDATE trade_buckets SET High = @High, Low = @Low, Volume = @Volume, Open = @Open, Close = @Close, number_of_trades = @NumTrades, bucket_group = @GroupBy WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStringLocal))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sqlIns;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Time", TimeUtil.FromUnixTime((long)Time));
                    cmd.Parameters.AddWithValue("@High", High);
                    cmd.Parameters.AddWithValue("@Low", Low);
                    cmd.Parameters.AddWithValue("@Volume", Volume);
                    cmd.Parameters.AddWithValue("@Open", Open);
                    cmd.Parameters.AddWithValue("@Close", Close);
                    cmd.Parameters.AddWithValue("@NumTrades", NumTrades);
                    cmd.Parameters.AddWithValue("@GroupBy", Group.ToString());
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
        public override string ToJSON()
        {
            return "{" + string.Format("\"time\":\"{0}\",\"high\":\"{1}\",\"low\":\"{2}\",\"volume\":\"{3}\",\"open\":\"{4}\",\"close\":\"{5}\",\"number_of_trades\":\"{6}\",\"bucket_group\":\"{7}\"", DTime.ToString("yyyy-MM-dd HH:mm:ss.000"),
                High, Low, Volume, Open, Close, NumTrades, Group) + "}";
        }

        private void merge(TradeBucket tb)
        {
            TradeBucket exist = (TradeBucket)tb;
            Id = exist.Id;

        }
        
    }
}
