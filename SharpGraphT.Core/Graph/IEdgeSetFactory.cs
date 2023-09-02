using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    public interface IEdgeSetFactory<TV, TE>
        where TE : class, new()
    {
        IEnumerable<TE> CreateEdgeSet(TV vertex);   
    }
}