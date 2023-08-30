using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph.builder
{
    public class GraphTypeBuilder<TV, TE>
        where TE : class, new()
    {
        private bool _undirected;
        private bool _directed;
        private bool _weighted;
        private bool _allowingMultipleEdges;
        private bool _allowingSelfLoops;
        private Func<TV> _vertexSupplier;
        private Func<TE> _edgeSupplier;

        private GraphTypeBuilder(bool directed, bool undirected)
        {
            _directed = directed;
            _undirected = undirected;
            _weighted = false;
            _allowingMultipleEdges = false;
            _allowingSelfLoops = false;
        }

        public static GraphTypeBuilder<TV, TE> Directed() => new GraphTypeBuilder<TV, TE>(true, false);

        public static GraphTypeBuilder<TV, TE> Undirected() => new GraphTypeBuilder<TV, TE>(false, true);

        public static GraphTypeBuilder<TV, TE> Mixed() => new GraphTypeBuilder<TV, TE>(true, true);

        public static GraphTypeBuilder<TV, TE> ForGraphType(IGraphType type)
        {
            GraphTypeBuilder<TV, TE> builder = new GraphTypeBuilder<TV, TE>(
                type.IsDirected || type.IsMixed, type.IsUndirected || type.IsMixed);
            builder._weighted = type.IsWeighted;
            builder._allowingSelfLoops = type.IsAllowingSelfLoops;
            builder._allowingMultipleEdges = type.IsAllowingMultipleEdges;
            return builder;
        }

        public static GraphTypeBuilder<TV, TE> ForGraph(IGraph<TV, TE>? graph)
        {
            GraphTypeBuilder<TV, TE> builder = ForGraphType(graph.GetType());
            builder._vertexSupplier = graph.GetVertexSupplier();
            builder._edgeSupplier = graph.GetEdgeSupplier();
            return builder;
        }

        public GraphTypeBuilder<TV, TE> Weighted(bool weighted)
        {
            this._weighted = weighted;
            return this;
        }

        public GraphTypeBuilder<TV, TE> AllowingSelfLoops(bool allowingSelfLoops)
        {
            this._allowingSelfLoops = allowingSelfLoops;
            return this;
        }

        public GraphTypeBuilder<TV, TE> AllowingMultipleEdges(bool allowingMultipleEdges)
        {
            this._allowingMultipleEdges = allowingMultipleEdges;
            return this;
        }

        //以下两个方法与jgrapht源码不同，因为C#泛型与java泛型不同， 以及省略了vertexClass和edgeClass这两个方法
        public GraphTypeBuilder<TV, TE> VertexSupplier(Func<TV> vertexSupplier)
        {
            _vertexSupplier = vertexSupplier;
            return (GraphTypeBuilder<TV, TE>)this;
        }

        public GraphTypeBuilder<TV, TE> EdgeSupplier(Func<TE> edgeSupplier)
        {
            _edgeSupplier = edgeSupplier;
            return (GraphTypeBuilder<TV, TE>)this;
        }

        public IGraphType buildType()
        {
            var TypeBuilder = new DefaultGraphType.Builder();
            if (_directed && _undirected)
            {
                TypeBuilder = TypeBuilder.Mixed();
            } else if (_directed)
            {
                TypeBuilder = TypeBuilder.Directed();
            } else if (_undirected)
            {
                TypeBuilder = TypeBuilder.Undirected();
            }

            return TypeBuilder
                .AllowMultipleEdges(_allowingMultipleEdges)
                .AllowSelfLoops(_allowingSelfLoops)
                .Weighted(_weighted)
                .Build();
        }

        public GraphBuilder<TV, TE, IGraph<TV, TE>> BuildGraphBuilder() => new GraphBuilder<TV, TE, IGraph<TV, TE>>(BuildGraph());

        public IGraph<TV, TE> BuildGraph()
        {
            if(_directed && _undirected)
            {
                throw new NotSupportedException("Mixed graph is not supported.");
            } else if (_directed){
                if(_allowingMultipleEdges && _allowingSelfLoops)
                {
                    _weighted ? return new DirectedWeightedPseudograph<TV, TE>(_vertexSupplier, _edgeSupplier)
                        : return new DirectedPseudograph<TV, TE>(_vertexSupplier, _edgeSupplier, false);
                }
            }
        }
    }
}
