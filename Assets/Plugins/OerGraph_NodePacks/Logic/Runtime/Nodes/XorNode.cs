using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Logic.Runtime.Nodes
{
    public class XorNode : OerNode
    {
        public override string Name { get; } = "^";
        protected override OerNode CreateInstance() => new AndNode();

        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Bool", "A", OerPortType.Input, false),
                ("Bool", "B", OerPortType.Input, false),
                ("Bool", "Result", OerPortType.Output, false),
            });
        }
        
        public override object GetValue(string portName)
        {
            var a = GetPort<OerBoolPort>("A").GetValue();
            var b = GetPort<OerBoolPort>("B").GetValue();

            return a ^ b;
        }
    }
}