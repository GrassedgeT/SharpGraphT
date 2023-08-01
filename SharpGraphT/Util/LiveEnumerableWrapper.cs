namespace SharpGraphT.Core.Util
{
    internal class LiveEnumerableWrapper<TE> where TE : class, new()
    {
        private Func<IReadOnlySet<TE>> edgeSet;

        public LiveEnumerableWrapper(Func<IReadOnlySet<TE>> edgeSet)
        {
            this.edgeSet = edgeSet;
        }
    }
}