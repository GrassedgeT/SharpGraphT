namespace SharpGraphT.Core.Graph
{
    public class DefaultEdge : IntrusiveEdge
    {
        private static readonly long serialVersionUID = 3258408452177932855L;

        protected Object GetSource() => base._source;
        
        protected Object GetTarget() => base._target;

        public override String ToString() => $"(source:{GetSource()},target:{GetTarget()})";
    }
}