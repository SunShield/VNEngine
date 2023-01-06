using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerSetStringVariableValueNode : OerSetVariableValueNode
    {
        protected override OerNode CreateInstance() => new OerSetStringVariableValueNode();
        public override string Name { get; } = "Set String Node";

        protected override OerNodePortsData GetAdditionalPortsData()
        {
            return new(new()
            {
                ("String", "Value", OerPortType.Input, false)
            });
        }

        public override object Resolve()
        {
            MainGraph.StringVariables[VariableName].CurrentValue = GetPort<OerStringPort>("Value").GetValue();
            return null;
        }
    }
}