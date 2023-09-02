using SharpGraphT.Core.Graph.specifics;

namespace SharpGraphT.Core.Graph;

[Serializable]
public class FastLookupGraphSpecificsStrategy<TV, TE> : IGraphSpecificsStrategy<TV, TE>
    where TE : class, new()
{
    private static readonly long serialVersionUID = -5490869870275054280L;

    public Func<IGraphType, IIntrusiveEdgesSpecifics<TV, TE>> GetIntrusiveEdgesSpecifics()
        => type => type.IsWeighted
            ? new WeightedIntrusiveEdgesSpecifics<TV, TE>(new Dictionary<TV, IntrusiveWeightedEdge>())
            : new UnweightedIntrusiveEdgesSpecifics<TV, TE>(new Dictionary<TV, IntrusiveEdge>());
       

   

    public Func<IGraph<TV, TE>, IGraphType, ISpecifics<TV, TE>> GetSpecificsFactory() 
        => (graph, type) => type.IsDirected
            ? new FastLookupDirectedSpecifics<TV, TE>(graph, 
                new Dictionary<TV, DirectedEdgeContainer<TV, TE>>(),
                new Dictionary<(TV, TV), ISet<TE>>(),
                ((IGraphSpecificsStrategy<TV, TE>)this).GetEdgeSetFactory())
            : new FastLookupUndirectedSpecifics<TV, TE>(graph,
                new Dictionary<TV, UndirectedEdgeContainer<TV, TE>>(),
                new Dictionary<(TV, TV), ISet<TE>>(),
                ((IGraphSpecificsStrategy<TV, TE>)this).GetEdgeSetFactory());   
}
