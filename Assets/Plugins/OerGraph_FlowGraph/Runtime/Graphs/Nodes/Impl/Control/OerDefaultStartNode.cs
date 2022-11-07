using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Control
{
    public class OerDefaultStartNode : OerFlowNode
    {
        public override string StartingNodeKey { get; } = "Start";
        public override string Name { get; } = "Start";
        public override object GetValue(string portName) => null;

        protected override OerNode CreateInstance() => new OerDefaultStartNode();
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Flow", "Out", OerPortType.Output, false)
            });
        }

        public override object Resolve() => null;
        public override OerFlowNode GetNextNode(object resolvationPayload) => GetPort<OerFlowPort>("Out").NextNode;
    }
}