using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeBucketCache;

namespace BitcoinTrader.Library.BatchProcesses.Batch
{
    public abstract class BatchProcess
    {
        protected object _process;
        protected ManualResetEvent _doneEvent;
        protected Cache _cache;

        public BatchProcess(Cache cache, object process)
        {
            _cache = cache;
            _process = process;
        }

        public abstract void Run();


        public void SetDoneEvent(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }
        public void ThreadPoolCallback(Object threadContext)
        {
            this.Run();
            _doneEvent.Set();
        }
    }
}
