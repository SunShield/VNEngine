using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics
{
    public class DivisionNode : OerNode
    {
        public override string Name { get; } = "/";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Dividend", OerPortType.Input, false),
                ("Float", "Divisor", OerPortType.Input, false),
                ("Float", "Fraction", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var dividend = GetPort<OerFloatPort>("Dividend").GetValue();
            var divisor = GetPort<OerFloatPort>("Divisor").GetValue();

            return dividend / divisor;
        }

        protected override OerNode CreateInstance() => new DivisionNode();
    }
}