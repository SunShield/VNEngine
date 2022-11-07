using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics
{
    public class SubtractNode : OerNode
    {
        public override string Name { get; } = "-";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Minuend", OerPortType.Input, false),
                ("Float", "Subtrahends", OerPortType.Input, true),
                ("Float", "Difference", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var minuend = GetPort<OerFloatPort>("Minuend").GetValue();
            var subtrahends = GetDynPort<OerFloatDynamicPort>("Subtrahends");
            foreach (var runtimePort in subtrahends.RuntimePorts)
            {
                minuend -= runtimePort.GetValue();
            }

            return minuend;
        }

        protected override OerNode CreateInstance() => new SubtractNode();
    }
}