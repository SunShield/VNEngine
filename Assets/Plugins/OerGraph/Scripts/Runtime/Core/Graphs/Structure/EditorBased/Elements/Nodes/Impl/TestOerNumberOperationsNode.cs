using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class TestOerNumberOperationsNode : OerNode
    {
        public override OerNodePortsData GetPortsData()
        {
            return new( new () 
            {
                ("Int", "Number1", OerPortType.Input),
                ("Int", "Number2", OerPortType.Input),
                ("Int", "Out1", OerPortType.Output),
                ("Int", "Out2", OerPortType.Output),
            });
        }

        public override object GetValue(string portName)
        {
            var number1 = GetPort<IntOerPort>("Number1").GetValue();
            var number2 = GetPort<IntOerPort>("Number2").GetValue();
            
            return portName switch
            {
                "Out1" => number1 + number2,
                "Out2" => number1 - number2,
                _ => default
            };
        }

        protected override OerNode CreateInstance() => new TestOerNumberOperationsNode();
    }
}