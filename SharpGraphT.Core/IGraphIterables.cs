using SharpGraphT.Util;

namespace SharpGraphT
{
    /// <summary>
    /// 将图表示为视图集合，适用于包含非常多顶点或边的图。使用这些方法编写的图算法可以使用不受32位算术限制的图。
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    /// <typeparam name="TE"></typeparam>
    public interface IGraphIterables<TV, TE> where TE : class, new()
    {
        IGraph<TV, TE> Graph { get; }
        
        IEnumerable<TE> Edges => 
            new LiveEnumerableWrapper<TE>(() => Graph.EdgeSet());
        
        long EdgeCount => Graph.EdgeSet().Count;

        IEnumerable<TV> Vertices => 
            new LiveEnumerableWrapper<TV>(() => Graph.VertexSet());

        long VertexCount => Graph.VertexSet().Count;

        IEnumerable<TE> EdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => Graph.EdgesOf(vertex));

        long DegreeOf(TV vertex) => Graph.DegreeOf(vertex);

        IEnumerable<TE> IncomingEdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => Graph.IncomingEdgesOf(vertex));   

        long InDegreeOf(TV vertex) => Graph.InDegreeOf(vertex);

        IEnumerable<TE> OutgoingEdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => Graph.OutgoingEdgesOf(vertex));

        long OutDegreeOf(TV vertex) => Graph.OutDegreeOf(vertex);

        IEnumerable<TE> allEdges(TV sourceVertex, TV targetVertex) => 
            new LiveEnumerableWrapper<TE>(() => 
            Graph.GetAllEdges(sourceVertex, targetVertex));
    }
}