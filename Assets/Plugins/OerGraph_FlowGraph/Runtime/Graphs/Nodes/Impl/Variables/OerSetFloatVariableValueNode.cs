using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerSetFloatVariableValueNode : OerSetVariableValueNode
    {
        protected override OerNode CreateInstance() => new OerSetFloatVariableValueNode();
        public override string Name { get; } = "Set Float Node";

        protected override OerNodePortsData GetAdditionalPortsData()
        {
            return new(new()
            {
                ("Float", "Value", OerPortType.Input, false)
            });
        }

        public override object Resolve()
        {
            MainGraph.FloatVariables[VariableName].CurrentValue = GetPort<OerFloatPort>("Value").GetValue();
            return null;
        }
    }
}