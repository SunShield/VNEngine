using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    [Serializable]
    public abstract class OerSetVariableValueNode : OerFlowNode
    {
        [field: SerializeField] protected string VariableName { get; private set; }
        
        public override object GetValue(string portName) => null;
        public void SetVariableName(string varName) => VariableName = varName;
        public override string StartingNodeKey { get; } = "";

        public sealed override OerNodePortsData GetPortsData()
        {
            var ports = new OerNodePortsData(new()
            {
                ("Flow", "In", OerPortType.Input, false),
                ("Flow", "Out", OerPortType.Output, false),
            });

            var additionalPorts = GetAdditionalPortsData();
            if (additionalPorts != null)
            {
                foreach (var additionalPort in additionalPorts.Ports)
                {
                    ports.Ports.Add(additionalPort);
                }
            }

            return ports;
        }

        protected virtual OerNodePortsData GetAdditionalPortsData() => null;

        public override OerFlowNode GetNextNode(object resolvationPayload) => GetPort<OerFlowPort>("Out").NextNode;
    }
}