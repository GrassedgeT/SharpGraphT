using SharpGraphT.Core.Graph;
using SharpGraphT.Core.Graph.specifics;

namespace SharpGraphT.Core.Graph;

public interface IGraphSpecificsStrategy<TV, TE>
    where TE : class, new()
{
    Func<IGraphType, IIntrusiveEdgesSpecifics<TV, TE>> GetIntrusiveEdgesSpecifics();
    Func<IGraph<TV, TE>, IGraphType, ISpecifics<TV, TE>> GetSpecificsFactory();
    IEdgeSetFactory<TV, TE> GetEdgeSetFactory() => new ArrayUnenforcedSetEdgeSetFactory<TV, TE>();
}
