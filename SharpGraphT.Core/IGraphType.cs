namespace SharpGraphT
{
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
}