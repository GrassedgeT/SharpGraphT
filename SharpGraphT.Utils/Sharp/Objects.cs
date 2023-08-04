using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraphT.Utils.Sharp;

public static class Objects
{
    //两个用于检查指定的对象引用是否为null的方法。对应仿照java中的Objects.requireNonNull(T obj)方法。
    public static T RequireNonNull<T>(T obj) where T : class? =>
        obj ?? throw new ArgumentNullException(nameof(obj));
    public static T RequireNonNull<T>(T obj, string message) where T : class? =>
        obj ?? throw new ArgumentNullException(message);
}
 