using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinTrader.Library.Business;
using MySql.Data.MySqlClient;
using LinqToTwitter;
using MtGoxTrader.MtGoxAPIClient;
using MtGoxTrader.Model;

namespace TestConsole
{
    class Program
    {
        const string connectionString = @"";
        
        static void Main(string[] args)
        {
            MtGoxAPIV0 v0 = new MtGoxAPIV0();
            v0.apiKey = "";
            v0.apiSecret = "";


            v0.buyBTC(0.01, MtGoxCurrencySymbol.USD, (double)5);
        }
        private static void tweetFor(Advice advice)
        {
            var auth = new SingleUserAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = "",
                    ConsumerSecret = "",
                    OAuthToken = "",
                    AccessToken = ""
                }
            };
            using (var twitterCtx = new TwitterContext(auth))
            {
                string status = advice.ToString();
                var tweet = twitterCtx.UpdateStatus(status.Substring(0, status.Length > 140 ? 140 : status.Length));
            }

        }
    }
}
