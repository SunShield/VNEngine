namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class IntOerPort : OerPort<int>
    {
        protected override OerPort<int> CreateInstance() => new IntOerPort();
    }
}