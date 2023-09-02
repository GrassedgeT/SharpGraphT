using SharpGraphT.Core.Graph;
using SharpGraphT.Core.Graph.specifics;

namespace SharpGraphT.Core;

public interface IGraphSpecificsStrateg<TV, TE>
    where TE : class, new()
{
    Func<IGraphType, IIntrusiveEdgesSpecifics<TV, TE>> GetIntrusiveEdgesSpecifics();
    Func<IGraph<TV, TE>, IGraphType, ISpecifics<TV, TE>> GetSpecificsFactory();
    IEdgeSetFactory<TV, TE> GetEdgeSetFactory() => new ArrayUnenforcedSetEdgeSetFactory<TV, TE>();
}
