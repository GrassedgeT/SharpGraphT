using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Util
{
    /// <summary>
    /// 用于构建一个一对一映射的帮助类，用于将顶点的集合映射到整数范围$[0, n)$，其中$n$是集合中顶点的数量。
    /// 此类仅在实例化时计算映射。它不支持实时更新。
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    public class VertexToIntegerMapping<TV>
    {
        public VertexToIntegerMapping(IEnumerable<TV> vertices)
        {
            throw new NotImplementedException();
        }

    }
}
