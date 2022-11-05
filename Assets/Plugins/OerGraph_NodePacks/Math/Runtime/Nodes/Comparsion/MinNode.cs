using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Comparsion
{
    public class MinNode : OerNode
    {
        public override string Name { get; } = "Min";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Numbers", OerPortType.Input, true),
                ("Float", "Min", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var numbers = GetDynPort<OerFloatDynamicPort>("Numbers");

            if (numbers.RuntimePorts.Count == 0) 
                throw new Exception($"Dyn Port {numbers.Name} must contain at least 1 port");

            var min = numbers.RuntimePorts[0].GetValue();
            foreach (var port in numbers.RuntimePorts)
            {
                if (min > port.GetValue())
                    min = port.GetValue();
            }

            return min;
        }

        protected override OerNode CreateInstance() => new MinNode();
    }
}