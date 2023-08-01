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
        IEnumerable Edges => new LiveEnumerableWrapper<TE>(Graph.EdgeSet);
        
    }
}