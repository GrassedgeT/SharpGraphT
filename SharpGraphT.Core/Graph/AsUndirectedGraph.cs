using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph;
[Serializable]
public class AsUndirectedGraph<TV, TE> : GraphDelegator<TV, TE>, IGraph<TV, TE>
    where TE : class, new()
{
    public AsUndirectedGraph(IGraph<TV, TE> g) : base(g)
    {

    }
}

