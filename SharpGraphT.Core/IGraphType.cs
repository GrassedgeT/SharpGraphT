namespace SharpGraphT;
/// <summary>
/// 一个图的类型。
/// 图的类型描述了图的各种属性，例如它是有向的，无向的还是混合的，它是否包含自环（自环是源顶点与目标顶点相同的边），
/// 它是否包含多个（平行）边（连接相同的一对顶点的多个边）以及它是否加权。
/// 图的类型可以使用方法Graph.getType()在运行时查询。例如，基于输入图是有向的还是无向的等，算法的行为可以有所不同。
/// </summary>
public interface IGraphType
{
    bool IsDirected { get; }
    bool IsUndirected { get; }
    bool IsMixed { get; }
    bool IsAllowingMultipleEdges { get; }
    bool IsAllowingSelfLoops { get; }
    bool IsAllowingCycles { get; }
    bool IsWeighted { get; }
    bool IsSimple { get; }
    bool IsPseudograph { get; }
    bool IsMultigraph { get; }
    bool IsModifiable { get; }
    IGraphType AsDirected();
    IGraphType AsUndirected();
    IGraphType AsMixed();
    IGraphType AsUnweighted();
    IGraphType AsWeighted();
    IGraphType AsModifiable();
    IGraphType AsUnmodifiable();
}
