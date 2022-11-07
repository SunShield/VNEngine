using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Ports;

namespace Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports
{
    public class OerFlowPort : OerPort<Flow>
    {
        protected override OerPort<Flow> CreateInstance() => new OerFlowPort();
        
        /// <summary>
        /// Runtime-only field. Contains a flow node connected to this port.
        /// Unlike regular OerPorts, Ouput Flow Ports can connect to exactly ONE Input Flow port (but not vice versa)
        ///
        /// This can be null - this means, this is a last node to execute on this path
        /// </summary>
        public OerFlowNode NextNode { get; private set; }

        protected override void PostInitializeRuntime()
        {
            var connections = MainGraph.GetConnectedPortIds(Id);
            if (connections == null || connections.Count == 0) return;
            if (connections.Count > 1)
                throw new Exception($"Port with id {Id} has more than one outgoing connections. This is not allowed for Flow ports");

            var connectedPort = MainGraph.GetPort(connections[0]);
            NextNode = MainGraph.GetNode(connectedPort.NodeId) as OerFlowNode;
        }
    }
}