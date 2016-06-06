using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using NTasker.Core.Tests.TestComponents;

namespace NTasker.Core.Tests
{
    /// <summary>
    /// Summary description for CompositionTests
    /// </summary>
    [TestClass]
    public class CompositionTests
    {
        public CompositionTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        private CompositionContainer _container;
        [Import(typeof(INTask))]
        public INTask _ntask;
        [TestMethod]
        public async Task BasicCompositionTest_ShouldLazyLoadINTaskImplementations()
        {
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CompositionTests).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
                Assert.IsNotNull(this._ntask);
                Assert.AreEqual(typeof(ConsoleWriterTask), _ntask.GetType());
                await this._ntask.Execute();
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
