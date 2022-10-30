namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class IntOerDynamicPort : OerDynamicPort<int>
    {
        public override string PortKey { get; } = "Int";
        protected override OerDynamicPort<int> CreateInstance() => new IntOerDynamicPort();
    }
}