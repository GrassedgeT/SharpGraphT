using System.ComponentModel;

namespace SharpGraphT.Core.Graph;

public class IntrusiveWeightedEdge : IntrusiveEdge
{
    private static readonly long serialVersionUID = 2890534758523920741L;

    private double weight = IGraph.DefaultEdgeWeight;
}