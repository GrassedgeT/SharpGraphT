using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Core.Graph
{
    /// <summary>
    /// 构造函数中指定的后备图的不可修改视图。此图允许模块向用户提供对内部图的“只读”访问。
    /// 此图上的查询操作“读取”到后备图，并且尝试修改此图会抛出异常。
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    /// <typeparam name="TE"></typeparam>
    public class AsUnmodifiableGraph<TV, TE> : GraphDelegator<TV, TE>
        where TE : class, new()
    {
        public AsUnmodifiableGraph(IGraph<TV, TE> g) : base(g) { }


    }
    
}
