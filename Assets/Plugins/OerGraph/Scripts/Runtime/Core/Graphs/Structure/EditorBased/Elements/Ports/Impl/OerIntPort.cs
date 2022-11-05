namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerIntPort : OerPort<int>
    {
        protected override OerPort<int> CreateInstance() => new OerIntPort();
    }
}