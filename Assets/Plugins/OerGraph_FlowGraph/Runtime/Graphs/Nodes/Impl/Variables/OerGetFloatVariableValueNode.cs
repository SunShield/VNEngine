using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerGetFloatVariableValueNode : OerGetVariableValueNode
    {
        public override object GetValue(string portName) => MainGraph.FloatVariables[VariableName].CurrentValue;
        protected override OerNode CreateInstance() => new OerGetFloatVariableValueNode();
        public override string Name { get; } = "Get Float Variable";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ( "Float", "Value", OerPortType.Output, false)
            });
        }
    }
}