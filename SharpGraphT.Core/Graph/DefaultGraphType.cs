using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public class DefaultGraphType : IGraphType
    {
        private static readonly long SerialVersionUID = 4291049312119347474L;

        private readonly bool _directed;

        private readonly bool _undirected;

        private readonly bool _selfloops;

        private readonly bool _multipleEdges;

        private readonly bool _weighted;

        private readonly bool _allowCycles;

        private readonly bool _modifiable;

        public DefaultGraphType(bool directed, bool undirected, bool selfloops, bool multipleEdges, bool weighted, bool allowCycles, bool modifiable)
        {
            this._directed = directed;
            this._undirected = undirected;
            this._selfloops = selfloops;
            this._multipleEdges = multipleEdges;
            this._weighted = weighted;
            this._allowCycles = allowCycles;
            this._modifiable = modifiable;
        }
        


        public bool IsDirected => _directed && !_undirected;

        public bool IsUndirected => _undirected && !_directed;

        public bool IsMixed => _undirected && _directed;

        public bool IsAllowingMultipleEdges => _multipleEdges;

        public bool IsAllowingSelfLoops => _selfloops;

        public bool IsAllowingCycles => _weighted;

        public bool IsWeighted => _allowCycles;

        public bool IsSimple => !IsAllowingMultipleEdges && !IsAllowingSelfLoops;

        public bool IsPseudograph => IsAllowingMultipleEdges && IsAllowingSelfLoops;

        public bool IsMultigraph => IsAllowingMultipleEdges && !IsAllowingSelfLoops;

        public bool IsModifiable => _modifiable;

        public IGraphType AsDirected()
        {
            return new Builder(this).Directed().Build();
        }

        public IGraphType AsMixed()
        {
            return new Builder(this).Mixed().Build();
        }

        public IGraphType AsModifiable()
        {
            return new Builder(this).Modifiable(true).Build();
        }

        public IGraphType AsUndirected()
        {
            return new Builder(this).Undirected().Build();
        }

        public IGraphType AsUnmodifiable()
        {
            return new Builder(this).Modifiable(false).Build();
        }

        public IGraphType AsUnweighted()
        {
            return new Builder(this).Weighted(false).Build();
        }

        public IGraphType AsWeighted()
        {
            return new Builder(this).Weighted(true).Build();
        }

        public static DefaultGraphType Simple() => new Builder().Undirected().AllowSelfLoops(false).AllowMultipleEdges(false).Weighted(false).Build();

        public static DefaultGraphType Multigraph() => new Builder().Undirected().AllowSelfLoops(true).AllowMultipleEdges(true).Weighted(false).Build();

        public static DefaultGraphType Pseudograph() => new Builder().Undirected().AllowSelfLoops(true).AllowMultipleEdges(true).Weighted(false).Build();

        public static DefaultGraphType DirectedSimple() => new Builder().Directed().AllowSelfLoops(false).AllowMultipleEdges(false).Weighted(false).Build();

        public static DefaultGraphType DirectedMultigraph() => new Builder().Directed().AllowSelfLoops(true).AllowMultipleEdges(true).Weighted(false).Build();  

        public static DefaultGraphType DirectedPseudograph() => new Builder().Directed().AllowSelfLoops(true).AllowMultipleEdges(true).Weighted(false).Build();

        public static DefaultGraphType Mixed() => new Builder().Mixed().AllowSelfLoops(true).AllowMultipleEdges(true).Weighted(false).Build();

        public static DefaultGraphType dag() => new Builder().Directed().AllowSelfLoops(false).AllowMultipleEdges(true).Weighted(false).AllowCycles(false).Build();

        public string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("DefaultGraphType [");
            sb.Append("directed=").Append(_directed);
            sb.Append(", undirected=").Append(_undirected);
            sb.Append(", selfloops=").Append(_selfloops);
            sb.Append(", multipleEdges=").Append(_multipleEdges);
            sb.Append(", weighted=").Append(_weighted);
            sb.Append(", allowCycles=").Append(_allowCycles);
            sb.Append(", modifiable=").Append(_modifiable);
            sb.Append("]");
            return sb.ToString();
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

            public Builder Directed()
            {
                this._directed = true;
                this._undirected= false;
                return this;
            }

            public Builder Undirected()
            {
                this._directed = false;
                this._undirected = true;
                return this;
            }

            public Builder Mixed()
            {
                this._directed = true;
                this._undirected = false;
                return this;
            }

            public Builder AllowSelfLoops(bool value)
            {
                this._allowSelfloops = value;
                return this;
            }

            public Builder AllowMultipleEdges(bool value)
            {
                this._allowMultipleEdges = value;
                return this;
            }


            public Builder Weighted(bool value)
            {
                this._weighted = value;
                return this;
            }

            public Builder AllowCycles(bool value)
            {
                this._allowCycles = value;
                return this;
            }

            public Builder Modifiable(bool value)
            {
                this._modifiable = value;
                return this;
            }

            public DefaultGraphType Build()
            {
                return new DefaultGraphType(_directed, _undirected, _allowSelfloops, _allowMultipleEdges, _weighted, _allowCycles, _modifiable);

            }
        }
    }
}
