using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTasker.Core.Tests.TestComponents
{
    [Export(typeof(INTask))]
    public class ConsoleWriterTask : INTask
    {

        public async Task Execute()
        {
            await (new TaskFactory()).StartNew(async () =>
            {
                Console.WriteLine("Writing to console ...");
            });
        }
    }
}
