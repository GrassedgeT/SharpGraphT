using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpGraphT;

/// <summary>
/// The root interface in the graph hierarchy.
/// 图层次结构中的根接口。
/// </summary>
public interface IGraph<TV, TE> where TE : class, new() {
    /// <summary>
    /// 边的默认权重。
    /// </summary>
    const double DefaultEdgeWeight = 1.0;

    /// <summary>
    /// 返回连接源顶点到目标顶点的所有边的集合，如果这样的顶点存在于这个图中。
    /// 如果任何一个顶点不存在或为null，则返回null。如果两个顶点都存在但没有找到边，则返回一个空集。
    /// </summary>
    /// <param name="sourceVertex">源顶点</param>
    /// <param name="targetVertex">目标顶点</param>
    /// <returns>返回连接源顶点到目标顶点的所有边的集合</returns>
    IReadOnlySet<TE> GetAllEdges(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 如果目标顶点和这样的边存在于这个图中，则返回连接源顶点到目标顶点的边。否则返回null。
    /// 如果指定的任何顶点为null，则返回null
    /// </summary>
    /// <param name="sourceVertex">源顶点</param>
    /// <param name="targetVertex">目标顶点</param>
    /// <returns>返回连接源顶点到目标顶点的边。</returns>
    TE GetEdge(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 返回图在需要创建新顶点时使用的顶点供应器（supplier）。
    /// </summary>
    /// <returns></returns>
    Func<TV> GetVertexSupplier();

    /// <summary>
    /// 返回图在需要创建新边时使用的边供应器（supplier）。
    /// </summary>
    /// <returns></returns>
    Func<TE> GetEdgeSupplier();

    /// <summary>
    /// 在这个图中创建一个新的边，从源顶点到目标顶点，并返回创建的边。
    /// 一些图不允许边多重性。在这种情况下，如果图已经包含从指定源到指定目标的边，则此方法不会更改图并返回null。
    /// </summary>
    /// <param name="sourceVertex">源顶点</param>
    /// <param name="targetVertex">目标顶点</param>
    /// <returns></returns>
    TE AddEdge(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 将指定的边添加到这个图中，从源顶点到目标顶点。更正式地说，如果这个图不包含任何边e2，使得e2.equals(e)，则将指定的边e添加到这个图中。如果这个图已经包含这样的边，调用将保持这个图不变并返回false。
    /// 一些图不允许边多重性。在这种情况下，如果图已经包含从指定源到指定目标的边，则此方法不会更改图并返回false。如果边被添加到图中，则返回true。
    /// </summary>
    /// <param name="sourceVertex">源顶点</param>
    /// <param name="targetVertex">目标顶点</param>
    /// <param name="e">将要添加的边</param>
    /// <returns></returns>
    bool AddEdge(TV sourceVertex, TV targetVertex, TE e);

    /// <summary>
    /// 创建一个新的顶点在这个图中并返回它。
    /// </summary>
    /// <returns></returns>
    TV AddVertex();

    /// <summary>
    /// 如果这个图中不存在此节点，则将其添加到这个图中。
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    bool AddVertex(TV v);

    /// <summary>
    /// 当且仅当这个图包含指定的边时，返回true。
    /// 在无向图中，当源和目标被反转时，得到相同的结果。如果任何指定的顶点在图中不存在，或者为null，则返回false。
    /// </summary>
    /// <param name="sourceVertex">源顶点</param>
    /// <param name="targetVertex">目标顶点</param>
    /// <returns></returns>
    bool ContainsEdge(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 当且仅当这个图包含指定的边 e 时，返回true。
    /// </summary>
    /// <param name="e">指定的边</param>
    /// <returns></returns>
    bool ContainsEdge(TE e);

    /// <summary>
    /// 如果此图包含指定的顶点，则返回true。
    /// </summary>
    /// <param name="v">指定的点</param>
    /// <returns></returns>
    bool ContainsVertex(TV v);

    /// <summary>
    /// 返回此图中包含的边的集合。该集合由图支持，因此图的更改反映在集合中。如果在对集合进行迭代时修改了图，则迭代的结果是未定义的。
    /// </summary>
    /// <returns>此图中包含的边的集合。</returns>
    IReadOnlySet<TE> EdgeSet();

    /// <summary>
    /// 返回指定顶点的度。
    /// 在无向图中，顶点的度是接触该顶点的边数。具有相同源和目标顶点（自循环）的边计数两次。
    /// </summary>
    /// <param name="vertex">指定的点</param>
    /// <returns>指定顶点的度。</returns>
    int DegreeOf(TV vertex);

    /// <summary>
    /// 返回接触指定顶点的所有边的集合。如果没有边接触指定的顶点，则返回一个空集。
    /// </summary>
    /// <param name="vertex">指定的边</param>
    /// <returns></returns>
    IReadOnlySet<TE> EdgesOf(TV vertex);

    /// <summary>
    /// 在有向图中，顶点的“入度”是从该顶点开始的内向有向边的数量。
    /// 在无向图的情况下，此方法返回接触顶点的边数。具有相同源和目标顶点（自循环）的边计数两次。
    /// </summary>
    /// <param name="vertex">指定的点</param>
    /// <returns></returns>
    int InDegreeOf(TV vertex);

    /// <summary>
    /// 返回所有进入指定顶点的边的集合。
    /// 在无向图的情况下，此方法返回接触顶点的所有边，因此，返回的某些边可能具有相反顺序的源和目标顶点。
    /// </summary>
    /// <param name="vertex">指定的点</param>
    /// <returns></returns>
    IReadOnlySet<TE> IncomingEdgesOf(TV vertex);

    /// <summary>
    /// 返回指定顶点的“出度”。
    /// 在有向图中，顶点的“出度”是从该顶点开始的外向有向边的数量。
    /// 在无向图的情况下，此方法返回接触顶点的边数。具有相同源和目标顶点（自循环）的边计数两次。
    /// </summary>
    /// <param name="vertex">指定的点</param>
    /// <returns>指定顶点的“出度”</returns>
    int OutDegreeOf(TV vertex);

    /// <summary>
    /// 返回从指定顶点出发的所有边的集合。
    /// </summary>
    /// <param name="vertex">指定的点</param>
    /// <returns></returns>
    IReadOnlySet<TE> OutgoingEdgesOf(TV vertex);

    /// <summary>
    /// 删除此图中也包含在指定边集合中的所有边。此调用返回后，此图将不包含与指定边相同的边。
    /// </summary>
    /// <typeparam name="TEe"></typeparam>
    /// <param name="edges">指定的边的集合</param>
    /// <returns></returns>
    bool RemoveAllEdges<TEe>(IEnumerable<TEe> edges) where TEe : TE;

    /// <summary>
    /// 删除从指定源顶点到指定目标顶点的所有边，并返回所有已删除边的集合。如果任何指定的顶点在图中不存在，则返回null。如果两个顶点都存在但找不到边，则返回一个空集。
    /// </summary>
    /// <param name="sourceVertex"></param>
    /// <param name="targetVertex"></param>
    /// <returns>被删除的边的集合</returns>
    IReadOnlySet<TE> RemoveAllEdges(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 删除此图中也包含在指定顶点集合中的所有顶点。此调用返回后，此图将不包含与指定顶点相同的顶点。
    /// </summary>
    /// <typeparam name="TVv"></typeparam>
    /// <param name="vertices"></param>
    /// <returns></returns>
    bool RemoveAllVertices<TVv>(IEnumerable<TVv> vertices) where TVv : TV;

    /// <summary>
    /// 删除从源顶点到目标顶点的边，如果这样的顶点和这样的边存在于此图中。如果删除，则返回边，否则返回null。
    /// </summary>
    /// <param name="sourceVertex"></param>
    /// <param name="targetVertex"></param>
    /// <returns></returns>
    TE RemoveEdge(TV sourceVertex, TV targetVertex);

    /// <summary>
    /// 删除指定的边，如果这样的边存在于此图中。如果成功删除，则返回true，否则返回false。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    bool RemoveEdge(TE e);

    /// <summary>
    /// 删除指定顶点及其所有相关边，如果该顶点存在于此图中。如果成功删除，则返回true，否则返回false。
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    bool RemoveVertex(TV v);

    /// <summary>
    /// 返回此图中包含的顶点集。该集合由图支持，因此图的更改反映在集合中。如果在对集合进行迭代时修改了图形，则迭代的结果是未定义的。
    /// </summary>
    /// <returns></returns>
    IReadOnlySet<TV> VertexSet();

    /// <summary>
    /// 返回边的源顶点。对于无向图，源和目标是可区分的指定（但没有任何数学意义）。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    TV GetEdgeSource(TE e);

    /// <summary>
    /// 返回边的目标顶点。对于无向图，源和目标是可区分的指定（但没有任何数学意义）。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    TV GetEdgeTarget(TE e);

    /// <summary>
    /// 获取图类型。图类型可用于查询其他元数据，例如图是否支持有向或无向边，自循环，多个（并行）边，权重等。
    /// </summary>
    /// <returns>图类型</returns>
    IGraphType GetType();

    /// <summary>
    /// 获取指定边的权重。如果图没有边权重，则返回默认权重值。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    double GetEdgeWeight(TE e);

    /// <summary>
    /// 为指定的边设置权重。如果图不支持边权重，则此调用将引发UnsupportedOperationException。
    /// </summary>
    /// <param name="e"></param>
    /// <param name="weight"></param>
    void SetEdgeWeight(TE e, double weight);

    void SetEdgeWeight(TV sourceVertex, TV targetVertex, double weight)
    {
        SetEdgeWeight(GetEdge(sourceVertex, targetVertex), weight);
    }

    /// <summary>
    /// 使用GraphIterables接口访问图。这允许访问图，而不受32位算术的限制。此外，图形实现可以自由实现此接口，而无需显式实现中间结果。
    /// </summary>
    IGraphIterables<TV, TE> Iterables => new DefaultGraphIterables<TV, TE>(this);
    
    bool GraphEquals(IGraph<TV, TE> graph);
}









