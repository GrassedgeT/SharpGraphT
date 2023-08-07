using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    public abstract class AbstractGraph<TV, TE> : IGraph<TV, TE>
        where TE : class, new()
    {
        
    }
}
