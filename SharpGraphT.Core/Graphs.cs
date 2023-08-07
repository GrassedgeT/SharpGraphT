using SharpGraphT.Core.Graph;
using SharpGraphT.Core.Util;
using SharpGraphT.Utils.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core;

/// <summary>
/// 用于辅助图操作的实用程序集。
/// </summary>
public abstract class Graphs
{
    public static TE AddEdge<TV, TE>(IGraph<TV, TE> g, TV sourceVertex, TV targetVertex, double weight)
        where TE : class, new()
    {
        Func<TE> edgeSupplier = g.GetEdgeSupplier();
        if (edgeSupplier == null)
            throw new ArgumentNullException("Graph contains no edge supplier");
        TE e = edgeSupplier();
        if (g.AddEdge(sourceVertex, targetVertex, e))
        {
            g.SetEdgeWeight(e, weight);
            return e;
        }else {
            return null;
        }
    }
    
    public static TE AddEdgeWithVertices<TV, TE>(IGraph<TV, TE> g, TV sourceVertex, TV targetVertex)
        where TE : class, new()
    {
        g.AddVertex(sourceVertex);
        g.AddVertex(targetVertex);
        return g.AddEdge(sourceVertex, targetVertex);
    }

    public static bool AddEdgeWithVertices<TV,TE>(IGraph<TV, TE> targetGraph, IGraph<TV, TE> sourceGraph, TE edge)
        where TE : class, new()
    {
        TV sourceVertex = sourceGraph.GetEdgeSource(edge);
        TV targetVertex = sourceGraph.GetEdgeTarget(edge);
        
        targetGraph.AddVertex(sourceVertex);
        targetGraph.AddVertex(targetVertex);

        return targetGraph.AddEdge(sourceVertex, targetVertex, edge);
    }

    public static TE AddEdgeWithVertices<TV, TE>(IGraph<TV, TE> g, TV sourceVertex, TV targetVertex, double weight)
        where TE : class, new()
    {
        g.AddVertex(sourceVertex);
        g.AddVertex(targetVertex);

        return AddEdge(g, sourceVertex, targetVertex, weight);
    }

    public static bool AddGraph<TV, TE, TVv, TEe>(IGraph<TV, TE> destination, IGraph<TVv, TEe> source)
        where TEe : class, TE, new()
        where TE : class, new()
        where TVv : TV
    {
        Boolean modified = AddAllVertices(destination, source.VertexSet());
        modified |= AddAllEdges(destination, source, source.EdgeSet());

        return modified;
    }

    public static void AddGraphReversed<TV, TE, TVv, TEe>(IGraph<TV, TE> destination, IGraph<TVv, TEe> source)
        where TEe : class, TE, new()
        where TE : class, new()
        where TVv : TV
    {
        if(!source.GetType().IsDirected || !destination.GetType().IsDirected)
        {
            throw new ArgumentException("Graphs must be directed");
        }

        AddAllVertices(destination, source.VertexSet());

        foreach (TEe e in source.EdgeSet())
        {
            destination.AddEdge(source.GetEdgeTarget(e), source.GetEdgeSource(e));
        }
    }

    public static bool AddAllEdges<TV, TE, TVv, TEe,Ee>(IGraph<TV, TE> destination, IGraph<TVv, TEe> source, IEnumerable<Ee> edges)
        where TEe : class, TE, new()
        where TE : class, new()
        where TVv : TV
        where Ee : TEe
    {
        bool modified = false;

        foreach (Ee e in edges)
        {
            TV s = source.GetEdgeSource(e);
            TV t = source.GetEdgeTarget(e);
            destination.AddVertex(s);
            destination.AddVertex(t);
            modified |= destination.AddEdge(s, t, e);
        }

        return modified;
    }

    public static bool AddAllVertices<TV, TE, Vv>(IGraph<TV, TE> destination, IEnumerable<Vv> vertices)
               where TE : class, new()
               where Vv : TV
    {
        bool modified = false;

        foreach (Vv v in vertices)
        {
            modified |= destination.AddVertex(v);
        }

        return modified;
    }

    public static List<TV> NeighborListOf<TV, TE>(IGraph<TV, TE> g, TV vertex)
        where TE : class, new()
    {
        List<TV> neighbors = new List<TV>();

        foreach (TE e in g.Iterables.EdgesOf(vertex))
        {
            neighbors.Add(GetOppositeVertex(g, e, vertex));
        }

        return neighbors;
    }

    public static ISet<TV> NeighborSetOf<TV, TE>(IGraph<TV, TE> g, TV vertex)
        where TE : class, new()
    {
        ISet<TV> neighbors = new HashSet<TV>();
        foreach( TE e in g.Iterables.EdgesOf(vertex))
        {
            neighbors.Add(GetOppositeVertex(g, e, vertex));
        }
        return neighbors;
    }

    public static List<TV> PredecessorListOf<TV, TE>(IGraph<TV, TE> g, TV vertex)
        where TE : class, new()
    {
        List<TV> predecessors = new List<TV>();

        foreach(TE e in g.Iterables.IncomingEdgesOf(vertex))
        {
            predecessors.Add(GetOppositeVertex(g, e, vertex));
        }

        return predecessors;
    }

    public static List<TV> SuccessorListOf<TV, TE>(IGraph<TV, TE> g, TV vertex)
        where TE : class, new()
    {
        List<TV> successors = new List<TV>();
        
        foreach(TE e in g.Iterables.OutgoingEdgesOf(vertex))
        {
            successors.Add(GetOppositeVertex(g, e, vertex));
        }

        return successors;
    }

    public static IGraph<TV, TE> UndirectedGraph<TV, TE>(IGraph<TV, TE> g)
        where TE : class, new()
    {
        if (g.GetType().IsDirected)
        {
            return new AsUndirectedGraph<TV, TE>(g);
        } else if (g.GetType().IsUndirected){
            return g;
        } else {
            throw new ArgumentException("Graph must be directed or undirected");
        }
    }

    public static bool testIncidence<TV, TE>(IGraph<TV, TE> g, TE e, TV v)
        where TE : class, new()
    {
        return (g.GetEdgeSource(e).Equals(v) || g.GetEdgeTarget(e).Equals(v));
    }

    public static TV GetOppositeVertex<TV, TE>(IGraph<TV, TE> g, TE e, TV v)
        where TE : class, new()
    {
        TV source = g.GetEdgeSource(e);
        TV target = g.GetEdgeTarget(e); 
        if (v.Equals(source))
        {
            return target;
        } else if (v.Equals(target))
        {
            return source;
        } else
        {
            throw new ArgumentException("no such vertex");
        }   
    }

    public static bool RemoveVertexAndPreserveConnectivity<TV, TE>(IGraph<TV, TE> graph, TV vertex)
        where TE : class, new()
    {
        if (!graph.ContainsVertex(vertex))
        {
            return false;
        }

        if(VertexHasPredecessors(graph, vertex))
        {
            List<TV> predecessors = PredecessorListOf(graph, vertex);
            List<TV> successors = SuccessorListOf(graph, vertex);

            foreach(TV predecessor in predecessors)
            {
                AddOutgoingEdges(graph, predecessor, successors);
            }
        }

        graph.RemoveVertex(vertex);
        return true;
    }

    public static bool RemoveVertexAndPreserveconnectivity<TV, TE>(IGraph<TV, TE> graph, Predicate<TV> predicate)
        where TE : class, new()
    {
        List<TV> verticesToRemove = new List<TV>();

        foreach (TV node in graph.VertexSet())
        {
            if (predicate(node))
            {
                verticesToRemove.Add(node);
            }
        }

        return RemoveVertexAndPreserveconnectivity(graph, verticesToRemove);
    }

    public static bool RemoveVertexAndPreserveconnectivity<TV, TE>(IGraph<TV, TE> graph, IEnumerable<TV> vertices)
        where TE : class, new()
    {
        bool atLeastOneVertexHasBeenRemoved = false;

        foreach (TV vertex in vertices)
        {
            if (RemoveVertexAndPreserveConnectivity(graph, vertex))
            {
                atLeastOneVertexHasBeenRemoved = true;
            }
        }

        return atLeastOneVertexHasBeenRemoved;
    }


    public static void AddOutgoingEdges<TV, TE>(IGraph<TV, TE> graph, TV source, IEnumerable<TV> targets)
        where TE : class, new()
    {
        if (!graph.ContainsVertex(source))
        {
            graph.AddVertex(source);
        }

        foreach(TV target in targets)
        {
            if(!graph.ContainsVertex(target))
            {
                graph.AddVertex(target);
            }
            graph.AddEdge(source, target);
        }
    }

    public static void AddIncomingEdges<TV, TE>(IGraph<TV, TE> graph, TV target, IEnumerable<TV> sources)
        where TE : class, new()
    {
        if (!graph.ContainsVertex(target))
        {
            graph.AddVertex(target);
        }

        foreach (TV source in sources)
        {
            if (!graph.ContainsVertex(source))
            {
                graph.AddVertex(source);
            }
            graph.AddEdge(source, target);
        }
    }

    public static bool VertexHasSuccessors<TV, TE>(IGraph<TV, TE> graph, TV vertex)
        where TE : class, new() => graph.Iterables.OutgoingEdgesOf(vertex).Any();

    public static bool VertexHasPredecessors<TV, TE>(IGraph<TV, TE> graph, TV vertex)
        where TE : class, new() => graph.Iterables.IncomingEdgesOf(vertex).Any();
    
    public static VertexToIntegerMapping<TV> getVertexToIntegerMapping<TV, TE>(IGraph<TV, TE> graph)
        where TE : class, new()
    {
        return new VertexToIntegerMapping<TV>(Objects.RequireNonNull(graph).VertexSet());
    }
} 

