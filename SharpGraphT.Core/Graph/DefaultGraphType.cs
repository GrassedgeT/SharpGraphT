using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public class Builder
        {
            private bool _directed;
            private bool _undirected;
            private bool _allowSelfloops;
            private bool _allowMultipleEdges;
            private bool _weighted;
            private bool _allowCycles;
            private bool _modifiable;

            public Builder()
            {
                this._directed = false;
                this._undirected = true;
                this._allowSelfloops = true;
                this._allowMultipleEdges = true;
                this._weighted = false;
                this._allowCycles = true;
                this._modifiable = true;
            }

            public Builder(IGraphType type)
            {
                this._directed = type.IsDirected || type.IsMixed;
                this._undirected = type.IsUndirected || type.IsMixed;
                this._allowSelfloops = type.IsAllowingSelfLoops;
                this._allowMultipleEdges = type.IsAllowingMultipleEdges;
                this._weighted = type.IsWeighted;
                this._allowCycles = type.IsAllowingCycles;
                this._modifiable = type.IsModifiable;
            }

            public Builder(bool directed, bool undirected)
            {
                if(!directed && !undirected)
                {
                    throw new ArgumentException("At least one of directed or undirected must be true");
                }
                _directed = directed;
                _undirected = undirected;
                _allowSelfloops = true;
                _allowMultipleEdges = true;
                _weighted = false;
                _allowCycles = true;
                _modifiable = true;
            }

            public Builder directed()
            {
                this._directed = true;
                this._undirected= false;
                return this;
            }

            public Builder undirected()
            {
                this._directed = false;
                this._undirected = true;
                return this;
            }

            public Builder mixed()
            {
                this._directed = true;
                this._undirected = false;
                return this;
            }
        }
    }
}
