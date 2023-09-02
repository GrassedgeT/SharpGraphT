using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGraphT.Core.Graph.specifics;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public class AbstractBasseGraph<TV, TE> : AbstractGraph<TV, TE>, IGraph<TV, TE>, ICloneable 
        where TE : class, new()
    {
        private static readonly long serialVersionUID = -3582386521833998627L;
        private const string GRAPH_SPECIFICS_MUST_NOT_BE_NULL = "Graph specifics must not be null";
        private const string INVALID_VERTEX_SUPPLIER_DOES_NOT_RETURN_UNIQUE_VERTICES_ON_EACH_CALL = "Invalid vertex supplier (does not return unique vertices on each call).";
        private const string MIXED_GRAPH_NOT_SUPPORTED = "Mixed graph not supported";
        private const string GRAPH_SPECIFICS_STRATEGY_REQUIRED = "Graph specifics strategy required";
        private const string THE_GRAPH_CONTAINS_NO_VERTEX_SUPPLIER = "The graph contains no vertex supplier";
        private const string THE_GRAPH_CONTAINS_NO_EDGE_SUPPLIER = "The graph contains no edge supplier";
        
        [NonSerialized] private IReadOnlySet<TV> _unmodifiableVertexSet;

        private Func<TV> _vertexSupplier;
        private Func<TE> _edgeSupplier;
        private IGraphType _type;
        private ISpecifics<TV, TE> _specifics; 
        private IIntrusiveEdgesSpecifics<TV, TE> _intrusiveEdgesSpecifics;
        private IGraphSpecificsStrategy<TV, TE> _graphSpecificsStrategy;
        [NonSerialized] private IGraphIterables<TV, TE> _graphIterables = null;

        protected AbstractBasseGraph(Func<TV> vertexSupplier, Func<TE> edgeSupplier, IGraphType type)
            : this(vertexSupplier, edgeSupplier, type, new DefaultGraphSpecificsStrategy<TV, TE>())
        protected AbstractBasseGraph(Func<TV> vertexSupplier, Func<TE> edgeSupplier, IGraphType type, IGraphSpecificsStrategy<TV, TE> graphSpecificsStrategy)
        {
            _vertexSupplier = vertexSupplier;
            _edgeSupplier = edgeSupplier;
            _type = type;
            _graphSpecificsStrategy = graphSpecificsStrategy;
            _specifics = graphSpecificsStrategy.GetSpecificsFactory()(this, type);
            _intrusiveEdgesSpecifics = graphSpecificsStrategy.GetIntrusiveEdgesSpecifics()(type);
        }

    }
}


