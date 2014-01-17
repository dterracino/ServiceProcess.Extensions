﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.ServiceProcess.Definitions
{
    /// <summary>
    /// Interactive task parallel service worker.
    /// </summary>
    public abstract class InteractiveTaskParallelServiceRunner : InteractiveServiceRunnerBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="workers"></param>
        protected InteractiveTaskParallelServiceRunner(IEnumerable<IServiceWorker> workers)
            : base(workers)
        {
            workers.VerifyServiceWorkers<ITaskParallelServiceWorker>();
        }

        /// <summary>
        /// Waits for the ServiceWorkers.
        /// </summary>
        /// <param name="workers"></param>
        protected sealed override void WaitForWorkers(params IServiceWorker[] workers)
        {
            //Wait for all the Workers Tasks to be completed.
            var tpsws = workers.OfType<ITaskParallelServiceWorker>();
            Task.WaitAll(tpsws.Select(x => x.Task).ToArray());
        }
    }
}
