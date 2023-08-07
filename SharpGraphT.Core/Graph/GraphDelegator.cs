using SharpGraphT.Core.Graph.builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    public class GraphDelegator<TV, TE> : AbstractGraph<TV, TE>, IGraph<TV, TE>
        where TE : class, new()
    {
        public GraphDelegator(IGraph<TV, TE> g)
        {
            throw new NotImplementedException();
        }
    }
}
