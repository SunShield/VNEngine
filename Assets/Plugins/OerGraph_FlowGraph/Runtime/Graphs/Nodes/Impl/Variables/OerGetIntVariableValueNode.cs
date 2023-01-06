using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    public class OerGetIntVariableValueNode : OerGetVariableValueNode
    {
        public override object GetValue(string portName) => MainGraph.IntVariables[VariableName].CurrentValue;
        protected override OerNode CreateInstance() => new OerGetIntVariableValueNode();
        public override string Name { get; } = "Get Int Variable";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ( "Int", "Value", OerPortType.Output, false)
            });
        }
    }
}