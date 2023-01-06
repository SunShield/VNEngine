using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerGetBoolVariableValueNode : OerGetVariableValueNode
    {
        public override object GetValue(string portName) => MainGraph.BoolVariables[VariableName].CurrentValue;
        protected override OerNode CreateInstance() => new OerGetBoolVariableValueNode();
        public override string Name { get; } = "Get Bool Variable";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ( "Bool", "Value", OerPortType.Output, false)
            });
        }
    }
}