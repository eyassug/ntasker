using NTasker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTasker
{
    /// <summary>
    /// Executes a single task on its own thread
    /// </summary>
    public class NTaskDaemon
    {
        CancellationToken _cancellationToken;
        INTask _task;
        int _timeoutInMilliseconds;

        public NTaskDaemon(CancellationToken cancellationToken, INTask task, int timeoutInMilliseconds)
        {
            _cancellationToken = cancellationToken;
            _task = task;
            _timeoutInMilliseconds = timeoutInMilliseconds;
        }

        public async Task Start()
        {
            var thread = new Thread(RunTask);
            //thread.
            thread.Start();
        }

        private async void RunTask()
        {
            while(!_cancellationToken.IsCancellationRequested)
            {
                await _task.Execute();
                Thread.Sleep(_timeoutInMilliseconds);
            }            
        }

        
    }
}
