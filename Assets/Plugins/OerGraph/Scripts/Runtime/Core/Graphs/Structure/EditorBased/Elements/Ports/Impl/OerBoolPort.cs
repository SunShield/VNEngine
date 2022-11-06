namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerBoolPort : OerPort<bool>
    {
        protected override OerPort<bool> CreateInstance() => new OerBoolPort();
    }
}