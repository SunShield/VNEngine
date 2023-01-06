using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerSetIntVariableValueNode : OerSetVariableValueNode
    {
        protected override OerNode CreateInstance() => new OerSetIntVariableValueNode();
        public override string Name { get; } = "Set Int Node";

        protected override OerNodePortsData GetAdditionalPortsData()
        {
            return new(new()
            {
                ("Int", "Value", OerPortType.Input, false)
            });
        }

        public override object Resolve()
        {
            MainGraph.IntVariables[VariableName].CurrentValue = GetPort<OerIntPort>("Value").GetValue();
            return null;
        }
    }
}