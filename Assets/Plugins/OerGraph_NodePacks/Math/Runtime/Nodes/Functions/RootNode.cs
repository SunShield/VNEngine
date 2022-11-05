using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;
using UnityEngine;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Functions
{
    public class RootNode : OerNode
    {
        public override string Name { get; } = "Sqrt";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Radicand", OerPortType.Input, false),
                ("Float", "Degree", OerPortType.Input, false),
                ("Float", "Root", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var radicand = GetPort<OerFloatPort>("Radicand").GetValue();
            var degree = GetPort<OerFloatPort>("Degree").GetValue();

            return Mathf.Pow(radicand, 1f / degree);
        }

        protected override OerNode CreateInstance() => new RootNode();
    }
}