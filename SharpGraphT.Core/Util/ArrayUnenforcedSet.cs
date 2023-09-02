namespace SharpGraphT.Core.Util;

[Serializable]
public class ArrayUnenforcedSet<TE> : List<TE>, ISet<TE>
    where TE : class, new()
{
    private static readonly long serialVersionUID = -7413250161201811238L;

    public ArrayUnenforcedSet() : base() { }

    public ArrayUnenforcedSet(IEnumerable<TE> collection) : base(collection) { }
    
    public ArrayUnenforcedSet(int i) : base(i) { }
    bool ISet<TE>.Add(TE item)
    {
        Add(item);
        return true;
    }
    
    /// <summary>
    /// jgrapht关于Equals和HashCode的解释是这样的：
    /// Note that for equals/hashCode, the class implements the Set behavior (unordered), not the list
    /// behavior (ordered); the fact that it subclasses ArrayList should be considered an implementation detail.
    /// 翻译：请注意，对于equals / hashCode，该类实现了Set行为（无序），而不是list行为（有序）； 应该将其子类化ArrayList的事实视为实现细节。
    /// 
    /// 但是在C#中，以我的能力无法实现，下面的重写是针对List行为实现的，与源码的定义不同
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ArrayUnenforcedSet<TE> other = (ArrayUnenforcedSet<TE>)obj;
        if (Count != other.Count)
        {
            return false;
        }

        for (int i = 0; i < Count; i++)
        {
            if (!this[i].Equals(other[i]))
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// 此处与源码实现也不同
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => base.GetHashCode();

    public void ExceptWith(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public void IntersectWith(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool IsProperSubsetOf(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool IsProperSupersetOf(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool IsSubsetOf(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool IsSupersetOf(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool Overlaps(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public bool SetEquals(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public void SymmetricExceptWith(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    public void UnionWith(IEnumerable<TE> other)
    {
        throw new NotImplementedException();
    }

    
}
