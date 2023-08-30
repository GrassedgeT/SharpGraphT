using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public class AbstractBasseGraph<TV, TE> : IGraph<TV, TE>, ICloneable
        where TE : class, new()
    {
    }
}
