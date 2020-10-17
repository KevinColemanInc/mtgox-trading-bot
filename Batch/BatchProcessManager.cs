using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.BatchProcesses.Batch
{
    public class BatchProcessManager
    {
        private List<BatchProcess> batches = new List<BatchProcess>();
        private List<ManualResetEvent> doneEvents = new List<ManualResetEvent>();
        private List<Thread> threads = new List<Thread>();
        public BatchProcessManager()
        {

        }

        public void AddBatch(BatchProcess proc)
        {

            batches.Add(proc);
            doneEvents.Add(new ManualResetEvent(false));
        }

        public void Start()
        {
            for (int i = 0; i < batches.Count; i++)
            {
                batches[i].SetDoneEvent(doneEvents[i]);
                Thread batchProc = new Thread(batches[i].ThreadPoolCallback);
                batchProc.Start();

                threads.Add(batchProc);
            }        
        }
        public void WaitForFinish()
        {
            for (int i = 0; i < batches.Count; i++)
            {
                Console.WriteLine(i);
                doneEvents[i].WaitOne();
            }
        }
    }
}
