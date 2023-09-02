using SharpGraphT.Core.Graph;
using SharpGraphT.Util;

namespace SharpGraphT.Core.Graph.specifics;
[Serializable]
public class ArrayUnenforcedSetEdgeSetFactory<TV, TE> : IEdgeSetFactory<TV, TE>
    where TE : class, new()
{
    private static readonly long serialVersionUID = 5936902837403445985L;

    public ISet<TE> CreateEdgeSet(TV vertex) => new ArrayUnenforcedSet<TE>(1);
    
}
    