using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerSetBoolVariableValueNode : OerSetVariableValueNode
    {
        protected override OerNode CreateInstance() => new OerSetBoolVariableValueNode();
        public override string Name { get; } = "Set Bool Node";

        protected override OerNodePortsData GetAdditionalPortsData()
        {
            return new(new()
            {
                ("Bool", "Value", OerPortType.Input, false)
            });
        }

        public override object Resolve()
        {
            MainGraph.BoolVariables[VariableName].CurrentValue = GetPort<OerBoolPort>("Value").GetValue();
            return null;
        }
    }
}