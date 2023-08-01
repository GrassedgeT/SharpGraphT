using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpGraphT;

public interface IGraph<TV, TE> where TE : class, new() {
    /* 
     * The default weight for an edge.
     * 边的默认权重。
     */
    const double DefaultEdgeWeight = 1.0;
    

    
}









