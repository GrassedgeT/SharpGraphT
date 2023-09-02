using System;
namespace SharpGraphT.Core.Graph;

public interface IIntrusiveEdgesSpecifics<TV, TE>
    where TE : class, new()
{
    TV GetEdgeSource(TE e);
    TV GetEdgeTarget(TE e);
    bool Add(TE e, TV sourceVertex, TV targetVertex);
    bool ContainsEdge(TE e);
    IReadOnlySet<TE> GetEdgesSet();
    void Remove(TE e);
    double GetEdgeWeight(TE e);
    void SetEdgeWeight(TE e, double weight);
}
