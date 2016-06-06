﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTasker.Core.Tests.TestComponents
{
    [Export(typeof(INTask))]
    [ExportMetadata("Frequency", (long)3000)]
    public class AnotherConsoleWriterTask : INTask
    {
        public async Task Execute()
        {
            await (new TaskFactory()).StartNew(async () =>
            {
                Console.WriteLine("Writing to console every 3 seconds...");
            });
        }
    }
}
