namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerBoolDynamicPort : OerDynamicPort<bool>
    {
        public override string PortKey { get; } = "Bool";
        protected override OerDynamicPort<bool> CreateInstance() => new OerBoolDynamicPort();
    }
}