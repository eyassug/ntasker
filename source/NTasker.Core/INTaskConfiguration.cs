using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTasker.Core
{
    /// <summary>
    /// Base interface for configuring INTask implementations
    /// </summary>
    public interface INTaskConfiguration
    {
        /// <summary>
        /// Task frequency in milliseconds
        /// </summary>
        long Frequency { get; }
        
    }
}
