using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KevinColemanInc.Utilities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace BitcoinTrader.Library.Business
{
    public abstract class DataObject
    {
        private int Id;

        protected static string connectionStringRDS = ConfigurationManager.AppSettings["connectionStringRDS"];
        protected static string connectionStringLocal = ConfigurationManager.AppSettings["connectionStringLocal"];
        
        public static DataTable RunSELECT(string query, string connectionString)
        {
            int attempts = 0;
            DataTable dt = new DataTable();
            while (attempts < 10)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlDataAdapter adr = new MySqlDataAdapter(query, conn);
                        adr.Fill(dt);
                        adr.Dispose();
                        adr = null;
                    }
                    return dt;
                }
                catch (Exception e)
                {
                    attempts += 1;
                    Thread.Sleep(3000);
                }
            }
            return null;
        }
        public static DataTable RunSELECT(string query)
        {
            return RunSELECT(query, connectionStringLocal);
        }

        public void SaveNew()
        {
            DataObject exists = Exists();
            if (exists == null)
            {
                SQLSave();
            }
            else
            {
                Update(exists);
            }
        }
        public abstract string ToJSON();
        public abstract void SQLSave();
        public abstract void Update(DataObject exists);
        public abstract DataObject Exists();
    }
}
