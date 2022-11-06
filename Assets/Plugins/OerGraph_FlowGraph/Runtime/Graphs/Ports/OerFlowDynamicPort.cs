using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Ports
{
    public class OerFlowDynamicPort : OerDynamicPort<Flow>
    {
        public override string PortKey { get; } = "Flow";
        protected override OerDynamicPort<Flow> CreateInstance() => new OerFlowDynamicPort();
    }
}