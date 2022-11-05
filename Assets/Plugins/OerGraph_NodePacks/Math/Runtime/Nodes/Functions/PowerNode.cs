using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports.Impl;
using UnityEngine;

namespace OerGraph_NodePacks.Math.Runtime.Nodes.Functions
{
    public class PowerNode : OerNode
    {
        public override string Name { get; } = "Pow";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ("Float", "Base", OerPortType.Input, false),
                ("Float", "Exponent", OerPortType.Input, false),
                ("Float", "Power", OerPortType.Output, false),
            });
        }

        public override object GetValue(string portName)
        {
            var @base = GetPort<OerFloatPort>("Base").GetValue();
            var exponent = GetPort<OerFloatPort>("Exponent").GetValue();

            return Mathf.Pow(@base, exponent);
        }

        protected override OerNode CreateInstance() => new PowerNode();
    }
}