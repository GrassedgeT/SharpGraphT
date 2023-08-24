using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph.builder
{
    public class GraphBuilder<TV, TE, TG> : AbstractGraphBuilder<TV, TE, TG, GraphBuilder<TV, TE, TG>>
         where TG : IGraph<TV, TE>
         where TE : class, new()
    {
        public GraphBuilder(TG baseGraph) : base(baseGraph) { }

        protected override GraphBuilder<TV, TE, TG> Self() => this;
    }
}
