using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.Business
{
    public class Subscription : DataObject
    {
        public static List<string> FetchUserEmailsForAdvice(Advice advice)
        {
            string sqlQuery = string.Format("select u.email from subscriptions as s join users as u on s.user_id = u.id where algorithm_name = '{0}' and bucket_group = '{1}'",advice.AlgorithmName, advice.Group);

            List<string> emailAccounts = new List<string>();
            emailAccounts.Add("");
            
            return emailAccounts;
        }

        public override string ToJSON()
        {
            
            return null;
        }
        public override void SQLSave()
        {
        }
        public override void Update(DataObject exists)
        {
        }
        public override DataObject Exists()
        {
            
            return null;
        }
    }
}
