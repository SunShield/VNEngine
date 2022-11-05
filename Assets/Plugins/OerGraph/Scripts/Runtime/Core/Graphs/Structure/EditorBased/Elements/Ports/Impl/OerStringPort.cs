namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerStringPort : OerPort<string>
    {
        protected override OerPort<string> CreateInstance() => new OerStringPort();
    }
}