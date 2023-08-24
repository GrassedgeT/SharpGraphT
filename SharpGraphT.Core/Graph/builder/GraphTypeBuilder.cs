using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph.builder
{
    public interface GraphTypeBuilderBase
    {
        // 中间类型
    }
    public class GraphTypeBuilder<TV, TE> : GraphTypeBuilderBase
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

        public GraphTypeBuilder<TV1, TE> VertexSupplier<TV1>(Func<TV1> vertexSupplier)
            where TV1 : TV
        {
            GraphTypeBuilderBase newBuilder = this;
            this._vertexSupplier = vertexSupplier;
            return (GraphTypeBuilder<TV1, TE>)this;
        }

     
    }
}
