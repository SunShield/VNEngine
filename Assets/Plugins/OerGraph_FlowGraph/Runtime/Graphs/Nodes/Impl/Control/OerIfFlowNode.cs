using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;
using Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Control
{
    public class OerIfFlowNode : OerFlowNode
    {
        public override object GetValue(string portName) => null;

        protected override OerNode CreateInstance() => new OerIfFlowNode();

        public override string Name { get; } = "If";
        public override string StartingNodeKey { get; }
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Flow", "In", OerPortType.Input, false),
                ("Bool", "Condition", OerPortType.Input, false),
                ("Flow", "True", OerPortType.Output, false),
                ("Flow", "False", OerPortType.Output, false),
            });
        }

        public override object Resolve()
        {
            return GetPort<OerBoolPort>("Condition").GetValue();
        }

        public override OerFlowNode GetNextNode(object resolvationPayload)
        {
            var payloadTyped = (bool)resolvationPayload;
            return payloadTyped 
                ? GetPort<OerFlowPort>("True").NextNode 
                : GetPort<OerFlowPort>("False").NextNode;
        }
    }
}