using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinTrader.Library.BatchProcesses.Batch;
using BitcoinTrader.Library.Business;
using KevinColemanInc.Utilities;

namespace AdviceAlertProcess
{
    public class EmailAlertProcess : BatchProcess
    {
        static DateTime lastEmailSent = DateTime.MinValue;
        public EmailAlertProcess(object process)
            : base(null, process)
        {
        }

        public override void Run()
        {
            while (true)
            {
                List<Advice> recentAdvices = Advice.GetAllRecentAdvices();
                
                foreach (Advice recentAdvice in recentAdvices)
                {
                    if (shouldISend(recentAdvice))
                    {
                        string subject = string.Empty;
                        string body = string.Empty;

                        subject += string.Format("BCTrader {0}, {1} Alert", recentAdvice.AlgorithmName, recentAdvice.Action.ToString());
                        body += string.Format("${0:N5}--{1}--{2}--{3}",
                            recentAdvice.Price, TimeUtil.FromUnixTime((long)recentAdvice.Time), recentAdvice.Group.ToString(), recentAdvice.AlgorithmName, recentAdvice.Confidence);
                        
                        lastEmailSent = DateTime.Now;
                        MailUtil.SendEmail("", subject, body);
                    }
                }
                Thread.Sleep(1000 * 60 * 1);
            }
        }

        private bool shouldISend(Advice recentAdvice)
        {
            bool is_recent = recentAdvice.DTime.AddMinutes(6) > DateTime.Now;
            bool too_soon = lastEmailSent.AddMinutes(6) < DateTime.Now;
            bool is_good_advice = recentAdvice.AlgorithmName.Equals("StochRSITrader") && recentAdvice.Group.Equals(GroupBy.hour);

            return is_recent && too_soon && is_good_advice;
        }

    }
}
