using NTasker.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTasker
{
    public class NTaskHost
    {
        #region Fields
        private readonly string _name;
        private readonly ICollection<Assembly> _assembliesWithTasks;
        private readonly string _assemblyDirectory;
        private readonly AggregateCatalog _catalog;
        private CompositionContainer _compositionContainer;

        [ImportMany]
        private IEnumerable<Lazy<INTask, INTaskConfiguration>> _tasks;
        #endregion

        #region Constructors
        private NTaskHost()
        {
            _catalog = new AggregateCatalog();
        }       
        
        public NTaskHost(string name)
            : this()
        {
            _name = name;
        }

        public NTaskHost(string name, params Assembly[] assembliesWithTasks)
            : this(name)
        {           
            var catalogs = assembliesWithTasks.Select(a => new AssemblyCatalog(a));
            foreach (var catalog in catalogs)
            {
                _catalog.Catalogs.Add(catalog);
            }
        }

        public NTaskHost(string name, params string[] directoriesWithAssemblies)
            : this(name)
        {
            foreach(var directory in directoriesWithAssemblies)
            {
                if (System.IO.Directory.Exists(directory))
                    _catalog.Catalogs.Add(new DirectoryCatalog(directory));
            }
        }
        #endregion

        public async Task Init(CancellationToken token)
        {
            Console.WriteLine("Configuring host ...");
            Configure();
            Console.WriteLine("Configuring container ...");
            try
            {
                ConfigureContainer();
                Console.WriteLine("Container configured successfully ...");
                await RunTasks(token);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not compose host container. Exiting gracefully ...");
            }
        }

        private void ConfigureContainer()
        {
            _compositionContainer = new CompositionContainer(_catalog);
            _compositionContainer.ComposeParts(this);
        }

        private async Task RunTasks(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                foreach (Lazy<INTask, INTaskConfiguration> task in _tasks)
                {
                    //TODO: Use task.Metadata.Frequency to schedule execution
                    //TODO: Add separate threads to initiate task demons and run tasks in their own context
                    await task.Value.Execute();
                    Thread.Sleep(TimeSpan.FromMilliseconds(task.Metadata.Frequency));
                }
            }
        }

        public virtual void Configure() { }
    }
}
