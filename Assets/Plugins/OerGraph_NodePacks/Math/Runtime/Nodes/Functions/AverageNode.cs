using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Functions
{
    public class AverageNode : OerNode
    {
        public override string Name { get; } = "Avg";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Numbers", OerPortType.Input, true),
                ("Float", "Average", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var numbers = GetDynPort<OerFloatDynamicPort>("Numbers");

            if (numbers.RuntimePorts.Count == 0) 
                throw new Exception($"Dyn Port {numbers.Name} must contain at least 1 port");

            var sum = 0f;
            foreach (var port in numbers.RuntimePorts)
            {
                sum += port.GetValue();
            }

            return sum / numbers.RuntimePorts.Count;
        }

        protected override OerNode CreateInstance() => new AverageNode();
    }
}