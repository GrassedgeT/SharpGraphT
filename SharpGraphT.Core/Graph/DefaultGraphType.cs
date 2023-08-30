using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public class DefaultGraphType : IGraphType
    {
        public bool IsDirected => throw new NotImplementedException();

        public bool IsUndirected => throw new NotImplementedException();

        public bool IsMixed => throw new NotImplementedException();

        public bool IsAllowingMultipleEdges => throw new NotImplementedException();

        public bool IsAllowingSelfLoops => throw new NotImplementedException();

        public bool IsAllowingCycles => throw new NotImplementedException();

        public bool IsWeighted => throw new NotImplementedException();

        public bool IsSimple => throw new NotImplementedException();

        public bool IsPseudograph => throw new NotImplementedException();

        public bool IsMultigraph => throw new NotImplementedException();

        public bool IsModifiable => throw new NotImplementedException();

        public IGraphType AsDirected()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsMixed()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsModifiable()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsUndirected()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsUnmodifiable()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsUnweighted()
        {
            throw new NotImplementedException();
        }

        public IGraphType AsWeighted()
        {
            throw new NotImplementedException();
        }
    }
}
