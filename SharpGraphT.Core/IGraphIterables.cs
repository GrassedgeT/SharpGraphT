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
        IGraph<TV, TE> GetGraph();
        
        IEnumerable<TE> Edges => 
            new LiveEnumerableWrapper<TE>(() => GetGraph().EdgeSet());
        
        long EdgeCount => GetGraph().EdgeSet().Count;

        IEnumerable<TV> Vertices => 
            new LiveEnumerableWrapper<TV>(() => GetGraph().VertexSet());

        long VertexCount => GetGraph().VertexSet().Count;

        IEnumerable<TE> EdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => GetGraph().EdgesOf(vertex));

        long DegreeOf(TV vertex) => GetGraph().DegreeOf(vertex);

        IEnumerable<TE> IncomingEdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => GetGraph().IncomingEdgesOf(vertex));   

        long InDegreeOf(TV vertex) => GetGraph().InDegreeOf(vertex);

        IEnumerable<TE> OutgoingEdgesOf(TV vertex) => 
            new LiveEnumerableWrapper<TE>(() => GetGraph().OutgoingEdgesOf(vertex));

        long OutDegreeOf(TV vertex) => GetGraph().OutDegreeOf(vertex);

        IEnumerable<TE> allEdges(TV sourceVertex, TV targetVertex) => 
            new LiveEnumerableWrapper<TE>(() =>
            GetGraph().GetAllEdges(sourceVertex, targetVertex));
    }
}