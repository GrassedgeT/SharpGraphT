namespace SharpGraphT.Core.Graph.specifics;

public interface ISpecifics<TV, TE>
    where TE : class, new()
{
    bool AddVertex(TV vertex);
    IEnumerable<TV> GetVertexSet();
    IEnumerable<TE> GetAllEdges(TV sourceVertex, TV targetVertex);
    TE GetEdge(TV sourceVertex, TV targetVertex);
    bool AddEdgeToTouchingVertices(TE edge, TV sourceVertex, TV targetVertex);
    bool AddEdgeToTouchingVerticesIfAbsent(TV sourceVertex, TV targetVertex, TE edge);
    TE CreateEdgeToTouchingVerticesIfAbsent(TV sourceVertex, TV targetVertex, Func<TV, TV, TE> edgeSupplier);
    int DegreeOf(TV vertex);
    IEnumerable<TE> EdgesOf(TV vertex);
    int InDegreeOf(TV vertex);
    IEnumerable<TE> IncomingEdgesOf(TV vertex);
    int OutDegreeOf(TV vertex);
    IEnumerable<TE> OutgoingEdgesOf(TV vertex);
    void RemoveEdgeFromTouchingVertices(TE edge, TV sourceVertex, TV targetVertex, TE e);
}

