using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Comparsion
{
    public class GreaterThenOrEqualsNode : OerNode
    {
        protected override OerNode CreateInstance() => new GreaterThenNode();
        public override string Name { get; } = ">=";

        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Number1", OerPortType.Input, false),
                ("Float", "Number2", OerPortType.Input, false),
                ("Bool",  "Result",  OerPortType.Output, false),
            });
        }
        
        public override object GetValue(string portName)
        {
            var number1 = GetPort<OerFloatPort>("Number1").GetValue();
            var number2 = GetPort<OerFloatPort>("Number2").GetValue();

            return number1 >= number2;
        }
    }
}