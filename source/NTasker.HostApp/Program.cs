using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTasker.HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
                        
            var hostName = "DefaultHost";
            // TODO: Get this from args parameter
            var sampleDirectory = @"..\..\..\NTasker.Core.Tests\bin\Debug";
            var host = new NTasker.NTaskHost(hostName, sampleDirectory);
            // Create a token that never expires
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            // Create a token that expires after N seconds
            // var tokenSource = new CancellationTokenSource(40 * 1000);
            // var token = tokenSource.Token;
            host.Init(token).Wait();            
            Console.Read();
        }
    }
}
