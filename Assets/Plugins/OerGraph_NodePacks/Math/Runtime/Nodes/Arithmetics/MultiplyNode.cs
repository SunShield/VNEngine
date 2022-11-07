using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics
{
    public class MultiplyNode : OerNode
    {
        public override string Name { get; } = "*";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Factors", OerPortType.Input, true),
                ("Float", "Product", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var factors = GetDynPort<OerFloatDynamicPort>("Factors");

            if (factors.RuntimePorts.Count == 0) 
                throw new Exception($"Dyn Port {factors.Name} must contain at least 1 port");

            var product = 1f;
            foreach (var port in factors.RuntimePorts)
            {
                product *= port.GetValue();
            }

            return product;
        }

        protected override OerNode CreateInstance() => new MultiplyNode();
    }
}