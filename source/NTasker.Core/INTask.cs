using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTasker.Core
{
    /// <summary>
    /// Base interface for INTasker that should be exposed by components
    /// </summary>
    public interface INTask
    {
        Task Execute();
    }
}
