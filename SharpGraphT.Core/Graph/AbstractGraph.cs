using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public abstract class AbstractGraph<TV, TE> : IGraph<TV, TE>
        where TE : class, new()
    {

        protected AbstractGraph()
        {
        }

        public bool RemoveAllEdges<TEe>(IEnumerable<TEe> edges) where TEe : TE
        {
            bool modified = false;

            foreach (var e in edges)
            {
                modified |= RemoveEdge(e);
            }

            return modified;
        }

        public IReadOnlySet<TE> RemoveAllEdges(TV sourceVertex, TV targetVertex)
        {
            IReadOnlySet<TE> removed = GetAllEdges(sourceVertex, targetVertex);
            if (removed == null) return null;
            RemoveAllEdges(removed);

            return removed;
        }

        public bool RemoveAllVertices<TVv>(IEnumerable<TVv> vertices) where TVv : TV
        {
            bool modified = false;

            foreach (var v in vertices)
            {
                modified |= RemoveVertex(v);
            }

            return modified;
        }

        protected bool AssertVertexExist(TV v)
        {
            if (ContainsVertex(v))
            {
                return true;
            }
            else if (v == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                throw new ArgumentException("no such vertex in graph" + v.ToString());
            }

            return true;
        }

        public override string ToString() => ToStringFromSets(VertexSet(), EdgeSet(), this.GetType().IsDirected);

        protected string ToStringFromSets<TVv, TEe>(IReadOnlySet<TVv> vertexSet, IReadOnlySet<TEe> edgeSet,
            bool isDirected)
            where TVv : TV
            where TEe : TE
        {
            List<string> renderedEdges = new List<string>();
            var sb = new StringBuilder();

            foreach (var e in edgeSet)
            {
                if (e is not DefaultEdge and not DefaultWeightedEdge)
                {
                    sb.Append(e.ToString());
                    sb.Append("=");
                }

                sb.Append(isDirected ? "(" : "{");
                sb.Append(GetEdgeSource(e));
                sb.Append(",");
                sb.Append(GetEdgeTarget(e));
                sb.Append(isDirected ? "）" : "}");
                renderedEdges.Add(sb.ToString());
                sb.Clear();
            }

            return "（" + vertexSet + ", " + renderedEdges + ")";
        }

        public override int GetHashCode()
        {
            int hash = VertexSet().GetHashCode();
            bool isDirected = this.GetType().IsDirected;

            foreach (var e in EdgeSet())
            {
                int part = e.GetHashCode();

                int source = GetEdgeSource(e).GetHashCode();
                int target = GetEdgeTarget(e).GetHashCode();

                int pairing = source + target;
                if (isDirected)
                {
                    pairing = ((pairing * (pairing + 1)) >> 1) + target;
                }

                part = (31 * part) + pairing;
                part = (31 * part) + GetEdgeWeight(e).GetHashCode();
                hash += part;
            }

            return hash;
        }

        //此处与jgrapht源码不同，源码设定是两个图形是相同图形类的实例，具有相同的顶点和边缘集以及相同的权重，这里直接调用可能会出现问题。
        public override bool Equals(Object obj) => base.Equals(obj);

        public abstract TE RemoveEdge(TV sourceVertex, TV targetVertex);

        public abstract bool RemoveEdge(TE e);

        public abstract bool RemoveVertex(TV v);

        public abstract void SetEdgeWeight(TE e, double weight);

        public abstract IReadOnlySet<TV> VertexSet();

        public abstract IGraphType GetType();

        public bool ContainsEdge(TV sourceVertex, TV targetVertex) => GetEdge(sourceVertex, targetVertex) != null;
        
        public abstract TE AddEdge(TV sourceVertex, TV targetVertex);

        public abstract bool AddEdge(TV sourceVertex, TV targetVertex, TE e);

        public abstract TV AddVertex();

        public abstract bool AddVertex(TV v);
        
        public abstract bool ContainsEdge(TE e);

        public abstract bool ContainsVertex(TV v);

        public abstract int DegreeOf(TV vertex);

        public abstract IReadOnlySet<TE> EdgeSet();

        public abstract IReadOnlySet<TE> EdgesOf(TV vertex);

        public abstract IReadOnlySet<TE> GetAllEdges(TV sourceVertex, TV targetVertex);

        public abstract TE GetEdge(TV sourceVertex, TV targetVertex);

        public abstract TV GetEdgeSource(TE e);

        public abstract Func<TE> GetEdgeSupplier();

        public abstract TV GetEdgeTarget(TE e);

        public abstract double GetEdgeWeight(TE e);

        public abstract Func<TV> GetVertexSupplier();

        public abstract bool GraphEquals(IGraph<TV, TE> graph);

        public abstract IReadOnlySet<TE> IncomingEdgesOf(TV vertex);

        public abstract int InDegreeOf(TV vertex);

        public abstract int OutDegreeOf(TV vertex);

        public abstract IReadOnlySet<TE> OutgoingEdgesOf(TV vertex);
    }
}
