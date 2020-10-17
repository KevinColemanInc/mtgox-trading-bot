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

    public class Advice : DataObject
    {
        public decimal Confidence { get; set; }
        public decimal Price { get; set; }
        public TradeAction Action { get; set; }
        public double Time { get; set; }
        public string AlgorithmName { get; set; }
        public GroupBy Group { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool EmailSent { get; set; }
        public int Id { get; set; }

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
        public Advice()
        {
            EmailSent = false;
        }

        public Advice(double time, string algorithm, GroupBy groupedBy)
        {
            Time = time;
            AlgorithmName = algorithm;
            Group = groupedBy; 
            EmailSent = false;
        }
        public static List<Advice> GetAllRecentAdvices()
        {
            List<Advice> advices = new List<Advice>();
            string query = string.Format(@"SELECT Time, Confidence, Price, Action, aa.algorithm_name, aa.bucket_group, Created_at FROM advices aa  INNER Join (SELECT algorithm_name, bucket_group, MAX(Time) as t FROM advices group by algorithm_name, bucket_group) bg on bg.bucket_group = aa.bucket_group AND bg.t = aa.Time AND aa.algorithm_name = bg.algorithm_name Order by Time Desc");
            DataTable dt = RunSELECT(query);
            foreach (DataRow dr in dt.Rows)
            {
                Advice advice = new Advice();
                advice.Time = (int)TimeUtil.ToUnixTime((DateTime)dr[0]);
                advice.Confidence = (decimal)dr[1];
                advice.Price = (decimal)dr[2];
                advice.Action = ActionUtil.GetTradeActionBy((string)dr[3]);
                advice.AlgorithmName = ((string)dr[4]).Trim();
                advice.Group = GroupUtil.GetGroupBy((string)dr[5]);
                advice.CreatedAt = (DateTime)dr[6];
                advices.Add(advice);
            }

            return advices;
        }

        public static double GetLastKnownTime(string algorithmName, GroupBy group)
        {
            DateTime last = DateTime.MinValue;
            string query = string.Format(@"SELECT Time FROM advices WHERE algorithm_name = '{1}' AND bucket_group = '{0}' Order By Time Desc limit 1", group.ToString(), algorithmName);
            DataTable dt = RunSELECT(query);

            if (dt.Rows.Count > 0)
                last = (DateTime)dt.Rows[0][0];


            if (last == DateTime.MinValue)
                return 0;

            return (double)TimeUtil.ToUnixTime(last);
        }

        public static List<Advice> GetAllAdvicesAfterTime(string algorithmName, GroupBy group, double start)
        {
            List<Advice> advices = new List<Advice>();

            string query = string.Format(@"SELECT Time, Confidence, Price, Action, algorithm_name, bucket_group, Created_at FROM advices WHERE Time >= '{0}' AND algorithm_name = '{1}' AND bucket_group = '{2}' Order By Time Asc", TimeUtil.FromUnixTime((long)start).ToString("yyyy-MM-dd HH:mm:ss"), algorithmName, group.ToString());
            DataTable dt = RunSELECT(query);

            foreach (DataRow dr in dt.Rows)
            {
                Advice advice = new Advice();
                advice.Time = (int)TimeUtil.ToUnixTime((DateTime)dr[0]);
                advice.Confidence = (decimal)dr[1];
                advice.Price = (decimal)dr[2];
                advice.Action = ActionUtil.GetTradeActionBy((string)dr[3]);
                advice.AlgorithmName = ((string)dr[4]).Trim();
                advice.Group = GroupUtil.GetGroupBy((string)dr[5]);
                advices.Add(advice);
            }

            return advices;
        }

        public static List<Advice> GetAllAdvicesInRange(string algorithmName, GroupBy group, double start, double end)
        {
            List<Advice> advices = new List<Advice>();

            string query = string.Format(@"SELECT Time, Confidence, Price, Action, algorithm_name, bucket_group, Created_at, id FROM advices WHERE Time >= '{0}' AND Time <= '{3}' AND algorithm_name = '{1}' AND bucket_group = '{2}' Order By Time Asc", TimeUtil.FromUnixTime((long)start).ToString("yyyy-MM-dd HH:mm:ss"), algorithmName, group.ToString(), TimeUtil.FromUnixTime((long)end).ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt = RunSELECT(query);

            foreach (DataRow dr in dt.Rows)
            {
                Advice advice = new Advice();
                advice.Time = (int)TimeUtil.ToUnixTime((DateTime)dr[0]);
                advice.Confidence = (decimal)dr[1];
                advice.Price = (decimal)dr[2];
                advice.Action = ActionUtil.GetTradeActionBy((string)dr[3]);
                advice.AlgorithmName = ((string)dr[4]).Trim();
                advice.Group = GroupUtil.GetGroupBy((string)dr[5]);
                advice.Id = (int)dr[6];
                advices.Add(advice);
            }
            return advices;
        }

        public static List<Advice> GetBackTestAdvice(string algorithmName, GroupBy group, double start, double end = -1)
        {
            List<Advice> allAdvices;

            if (end <= 0)
            {
                allAdvices = GetAllAdvicesAfterTime(algorithmName, group, start);
            }
            else
            {
                allAdvices = GetAllAdvicesInRange(algorithmName, group, start, end);
            }

            List<Advice> advices = new List<Advice>();
            bool hasCoins = false;

            foreach (Advice advice in allAdvices)
            {
                if (hasCoins && advice.Action == TradeAction.Sell)
                {
                    advices.Add(advice);
                    hasCoins = false;
                }
                else if (hasCoins == false && advice.Action == TradeAction.Buy)
                {
                    advices.Add(advice);
                    hasCoins = true;
                }
            }

            return advices;
        }

        public override DataObject Exists()
        {
            string query = string.Format(@"SELECT Time, Confidence, Price, Action, algorithm_name, bucket_group, Created_at, id, email_sent FROM advices WHERE Time = '{0}' AND algorithm_name = '{1}' AND bucket_group = '{2}'", TimeUtil.FromUnixTime((long)Time).ToString("yyyy-MM-dd HH:mm:ss"), AlgorithmName, Group.ToString());
            DataTable dt = RunSELECT(query);
            if (dt.Rows.Count > 0)
            {
                Advice advice = new Advice();
                advice.Time = (int)TimeUtil.ToUnixTime((DateTime)dt.Rows[0][0]);
                advice.Confidence = (decimal)dt.Rows[0][1];
                advice.Price = (decimal)dt.Rows[0][2];
                advice.Action = ActionUtil.GetTradeActionBy((string)dt.Rows[0][3]);
                advice.AlgorithmName = ((string)dt.Rows[0][4]).Trim();
                advice.Group = GroupUtil.GetGroupBy((string)dt.Rows[0][5]);
                advice.Id = (int)dt.Rows[0][7];
                advice.EmailSent = (bool)dt.Rows[0][8];
                return advice;
            }
            else
            {
                return null;
            }
        }

        public override void SQLSave()
        {
            string sqlIns = "INSERT INTO advices (Time, Confidence, Price, Action, algorithm_name, bucket_group, Created_at, updated_at, email_sent) VALUES (@Time, @Confidence, @Price, @Action, @AlgorithmName, @BucketGroup, @Created_at, @Created_at, @email_sent)";
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
                        cmd.Parameters.AddWithValue("@Confidence", Confidence);
                        cmd.Parameters.AddWithValue("@Price", Price);
                        cmd.Parameters.AddWithValue("@Action", Action.ToString());
                        cmd.Parameters.AddWithValue("@AlgorithmName", AlgorithmName);
                        cmd.Parameters.AddWithValue("@BucketGroup", Group.ToString());
                        cmd.Parameters.AddWithValue("@Created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@email_sent", EmailSent);
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
                    cmd.Parameters.AddWithValue("@Confidence", Confidence);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Action", Action.ToString());
                    cmd.Parameters.AddWithValue("@AlgorithmName", AlgorithmName);
                    cmd.Parameters.AddWithValue("@BucketGroup", Group.ToString());
                    cmd.Parameters.AddWithValue("@Created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@email_sent", EmailSent);
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
            merge((Advice)exists);
            string sqlIns = "UPDATE advices SET updated_at = @updated_at, Time = @Time, Confidence = @Confidence, Price = @Price, Action = @Action, algorithm_name = @AlgorithmName, bucket_group = @BucketGroup WHERE id = @Id";
            try
            {
                if (false)
                {
                    
                }
                using (MySqlConnection conn = new MySqlConnection(connectionStringLocal))
                {
                    conn.Open(); MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sqlIns;
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Time", TimeUtil.FromUnixTime((long)Time));
                    cmd.Parameters.AddWithValue("@Confidence", Confidence);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Action", Action.ToString());
                    cmd.Parameters.AddWithValue("@AlgorithmName", AlgorithmName);
                    cmd.Parameters.AddWithValue("@BucketGroup", Group.ToString());
                    cmd.Parameters.AddWithValue("@updated_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", Id);
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

        public override string ToString()
        {
            return string.Format("{0} says to {2} @ Price:${3:N4} at {1} #Bitcoin", AlgorithmName, DTime, Action, Price, Confidence);
        }

        public string ToCSV()
        {
            return string.Format("{0},{1},{2},{3}", Time, Action, Price, Confidence);
        }
        public override string ToJSON()
        {

            return "{" + string.Format("\"action\":\"{0}\",\"algorithm_name\":\"{1}\",\"bucket_group\":\"{2}\",\"confidence\":\"{3}\",\"price\":\"{4}\", \"time\":\"{5}\"", Action, AlgorithmName, Group, Confidence, Price, DTime.ToString("yyyy-MM-dd HH:mm:ss.000")) + "}";
        }
        private void merge(Advice advice)
        {
            Id = advice.Id;
        }
    }
}
