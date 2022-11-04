namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerIntDynamicPort : OerDynamicPort<int>
    {
        public override string PortKey { get; } = "Int";
        protected override OerDynamicPort<int> CreateInstance() => new OerIntDynamicPort();
    }
}