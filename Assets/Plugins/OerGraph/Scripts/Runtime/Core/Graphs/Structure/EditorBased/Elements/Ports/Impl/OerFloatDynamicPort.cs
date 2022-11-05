namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl
{
    public class OerFloatDynamicPort : OerDynamicPort<float>
    {
        public override string PortKey { get; } = "Float";
        protected override OerDynamicPort<float> CreateInstance() => new OerFloatDynamicPort();
    }
}