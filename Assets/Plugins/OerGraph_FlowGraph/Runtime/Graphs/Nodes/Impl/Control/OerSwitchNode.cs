using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;
using OerGraph_FlowGraph.Runtime.Graphs.Ports;
using Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Control
{
    public class OerSwitchNode : OerFlowNode
    {
        public override object GetValue(string portName) => null;
        protected override OerNode CreateInstance() => new OerSwitchNode();
        public override string Name { get; } = "Switch";
        public override string StartingNodeKey { get; }
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Flow", "In", OerPortType.Input, false),
                ("Bool", "Conditions", OerPortType.Input, true),
                ("Flow", "Outs", OerPortType.Output, true),
                ("Flow", "Default", OerPortType.Output, false),
            });
        }

        public override object Resolve()
        {
            var conditions = GetDynPort<OerBoolDynamicPort>("Conditions");
            for (int i = 0; i < conditions.RuntimePorts.Count; i++)
            {
                var conditionPort = conditions.RuntimePorts[i];
                var conditionTrue = conditionPort.GetValue();
                if (conditionTrue) return i;
            }

            return null;
        }

        public override OerFlowNode GetNextNode(object resolvationPayload)
        {
            if (resolvationPayload == null) return GetPort<OerFlowPort>("Default").NextNode;

            var firstTrueConditionIndex = (int)resolvationPayload;
            var outputs = GetDynPort<OerFlowDynamicPort>("Outs");
            return (outputs.RuntimePorts[firstTrueConditionIndex] as OerFlowPort).NextNode;
        }
    }
}