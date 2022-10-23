namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Ports.Impl
{
    public class StringOerPort : OerPort<string>
    {
        protected override OerPort<string> CreateInstance() => new StringOerPort();
    }
}