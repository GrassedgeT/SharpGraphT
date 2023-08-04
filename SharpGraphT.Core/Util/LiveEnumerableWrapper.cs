using SharpGraphT.Utils.Sharp;
using System.Collections;

namespace SharpGraphT.Util
{
    public class LiveEnumerableWrapper<TE> : IEnumerable<TE> 
    {
        private Func<IReadOnlySet<TE>>? _supplier;

        public LiveEnumerableWrapper() : this(null) { }

        public LiveEnumerableWrapper(Func<IReadOnlySet<TE>>? supplier)
        {
            _supplier = Objects.RequireNonNull(supplier);
        }

        public IEnumerator<TE> GetEnumerator()
        {
            return _supplier().GetEnumerator();
        }

        public Func<IReadOnlySet<TE>>? Supplier
        {
            get => _supplier;
            set => _supplier = value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}