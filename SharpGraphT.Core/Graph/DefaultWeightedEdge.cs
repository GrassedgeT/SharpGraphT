using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    public class DefaultWeightedEdge : IntrusiveWeightedEdge
    {
        private static readonly long serialVersionUID = -3259071493169286685L;

        protected Object GetSource() => _source;

        protected Object GetTarget() => _target;

        protected double GetWeight() => Weight;

        public override String ToString() => $"(source:{GetSource()},target:{GetTarget()},weight:{GetWeight()})";
    }
}
