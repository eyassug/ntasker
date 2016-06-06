using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace NTasker.Tests
{
    [TestClass]
    public class NTaskHostTests
    {
        [TestMethod]
        public void NTaskHost_InitializationTests()
        {
            var hostName = "DefaultHost";
            var sampleDirectory = @"C:\Users\Eyassu\Documents\Projects\Personal Open Source\NTasker\source\NTasker.Core.Tests\bin\Debug";
            var host = new NTasker.NTaskHost(hostName, sampleDirectory);
            Assert.IsNotNull(host.Name);
            Assert.AreEqual(hostName, host.Name);
            Assert.IsNotNull(host.Catalog);
            Assert.IsNull(host.Container);
        }

        [TestMethod]
        public async Task NTaskHost_ContainerShouldNotBeNullAfterInit()
        {
            var hostName = "DefaultHost";
            var sampleDirectory = @"C:\Users\Eyassu\Documents\Projects\Personal Open Source\NTasker\source\NTasker.Core.Tests\bin\Debug";
            var host = new NTasker.NTaskHost(hostName, sampleDirectory);
            var token = new CancellationToken(false);
            await host.Init(new CancellationToken(false));
            Assert.IsNotNull(host.Container);
        }


    }
}
