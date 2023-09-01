using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    [Serializable]
    public class IntrusiveEdge : ICloneable
    {
        private static readonly long serialVersionUID = 3258408452177932855L;

        protected Object _source;

        protected Object _target;

        public object Clone()
        {
            try
            {
                return base.MemberwiseClone();
            }
            catch (Exception e)
            {
                throw new ExecutionEngineException();
            }
        }
    }
}
