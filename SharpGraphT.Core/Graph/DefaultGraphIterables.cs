using SharpGraphT.Utils.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph;

/// <summary>
/// 图可迭代的默认实现，简单地委托给集合实现。
/// </summary>
/// <typeparam name="TV"></typeparam>
/// <typeparam name="TE"></typeparam>
public class DefaultGraphIterables<TV, TE> : IGraphIterables<TV, TE>
    where TE : class, new()
{
    protected IGraph<TV, TE>? graph;

    public IGraph<TV, TE> GetGraph() => graph;

    public DefaultGraphIterables() : this(null) { }

    public DefaultGraphIterables(IGraph<TV, TE>? graph)
    {
        this.graph = Objects.RequireNonNull(graph);
    }
}

