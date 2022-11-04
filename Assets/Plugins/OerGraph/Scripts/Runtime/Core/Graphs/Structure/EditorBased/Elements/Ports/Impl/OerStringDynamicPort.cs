namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerStringDynamicPort : OerDynamicPort<string>
    {
        public override string PortKey { get; } = "Int";
        protected override OerDynamicPort<string> CreateInstance() => new OerStringDynamicPort();
    }
}