using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics
{
    public class ModulationNode : OerNode
    {
        public override string Name { get; } = "%";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Int", "Dividend", OerPortType.Input, false),
                ("Int", "Divisor", OerPortType.Input, false),
                ("Int", "Remainder", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var dividend = GetPort<OerIntPort>("Dividend").GetValue();
            var divisor = GetPort<OerIntPort>("Divisor").GetValue();

            return dividend / divisor;
        }

        protected override OerNode CreateInstance() => new SumNode();
    }
}