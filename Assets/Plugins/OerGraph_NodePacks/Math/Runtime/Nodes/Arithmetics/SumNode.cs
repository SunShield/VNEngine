using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics
{
    public class SumNode : OerNode
    {
        public override string Name { get; } = "+";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Addends", OerPortType.Input, true),
                ("Float", "Sum", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var addends = GetDynPort<OerFloatDynamicPort>("Addends");

            if (addends.RuntimePorts.Count == 0) 
                throw new Exception($"Dyn Port {addends.Name} must contain at least 1 port");

            var sum = 0f;
            foreach (var port in addends.RuntimePorts)
            {
                sum += port.GetValue();
            }

            return sum;
        }

        protected override OerNode CreateInstance() => new SumNode();
    }
}