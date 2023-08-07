using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph.builder;

public abstract class AbstractGraphBuilder<TV, TE, TG, TB>
    where TG : IGraph<TV, TE>
    where TB : AbstractGraphBuilder<TV, TE, TG, TB>
    where TE : class, new()
{
    protected readonly TG Graph;

    public AbstractGraphBuilder(TG graph) => Graph = graph;

    protected abstract TB Self();

    public TB AddVertex(TV vertex)
    {
        Graph.AddVertex(vertex);
        return Self();
    }

    public TB AddVertices(params TV[] vertices)
    {
        foreach (TV vertex in vertices)
            AddVertex(vertex);
        return Self();
    }


    public TB AddEdge(TV source, TV target)
    {
         Graphs.AddEdgeWithVertices(Graph, source, target);
         return Self();
    }

    public TB AddEdge(TV source, TV target, TE edge)
    {
        AddVertex(source);
        AddVertex(target);
        Graph.AddEdge(source, target, edge);
        return Self();
    }

    public TB AddEdgeChain(TV first, TV second, params TV[] rest)
    {
        AddEdge(first, second);
        TV last = second;
        foreach (TV vertex in rest)
        {
            AddEdge(last, vertex);
            last = vertex;
        }
        return Self();
    }

    public TB AddGraph<TVv, TEe>(IGraph<TVv, TEe> sourceGraph)
        where TEe : class, TE, new()
        where TVv : TV
    {
        Graphs.AddGraph(Graph, sourceGraph);
        return Self();
    }

    public TB RemoveVertex(TV vertex)
    {
        Graph.RemoveVertex(vertex);
        return Self();
    }

    public TB RemoveVertices(params TV[] vertices)
    {
        foreach (TV vertex in vertices)
            RemoveVertex(vertex);
        return Self();
    }

    public TB RemoveEdge(TV source, TV target)
    {
        Graph.RemoveEdge(source, target);
        return Self();
    }

    public TB RemoveEdge(TE edge)
    {
        Graph.RemoveEdge(edge);
        return Self();
    }

    public TB AddEdge(TV source, TV target, double weight)
    {
        Graphs.AddEdge(Graph, source, target, weight);
        return Self();
    }

    public TB AddEdge(TV source, TV target, TE edge, double weight)
    {
        Graph.AddEdge(source, target, edge);
        Graph.SetEdgeWeight(edge, weight);
        return Self();
    }

    public TG build() => Graph;

    public IGraph<TV, TE> BuildAsUnmodifiable() => new AsUnmodifiableGraph<TV, TE>(Graph);

   
}

