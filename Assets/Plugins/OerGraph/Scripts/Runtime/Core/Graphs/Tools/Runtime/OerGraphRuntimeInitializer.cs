using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased
{
    public partial class OerMainGraph
    {
        public static class OerGraphRuntimeInitializer
        {
            private static readonly List<IOerPort> collectionForAllocations_ports = new();
            private static readonly List<IOerDynamicPort> collectionForAllocations_dynPorts = new();

            public static void InitializeGraphRuntime(OerMainGraph mainGraph)
            {
                foreach (var nodeId in mainGraph._nodes.Keys)
                {
                    InitializeNodeRuntime(mainGraph, nodeId);
                }
            }

            private static void InitializeNodeRuntime(OerMainGraph mainGraph, int nodeId)
            {
                var node = mainGraph._nodes[nodeId];
                var ports = GetNodePorts(mainGraph, node);
                var dynPorts = GetNodeDynamicPorts(mainGraph, node);
                node.InitializeRuntime(mainGraph, ports, dynPorts);

                foreach (var port in ports)
                {
                    InitializePortRuntime(mainGraph, port);
                }
                foreach (var dynamicPort in dynPorts)
                {
                    InitializeDynamicPortRuntime(mainGraph, dynamicPort);
                }
            }

            private static List<IOerPort> GetNodePorts(OerMainGraph mainGraph, OerNode node)
            {
                collectionForAllocations_ports.Clear();
                
                foreach (var inPortId in node.InPortIds)
                {
                    collectionForAllocations_ports.Add(mainGraph.GetPort(inPortId));
                }

                foreach (var outPortId in node.OutPortIds)
                {
                    collectionForAllocations_ports.Add(mainGraph.GetPort(outPortId));
                }

                return collectionForAllocations_ports;
            }

            private static void InitializePortRuntime(OerMainGraph mainGraph, IOerPort port)
            {
                var ownerNode = GetPortOwnerNode(mainGraph, port);
                var valueRetrievingPort = GetPortValueRetrievingPort(mainGraph, port);
                var valueRetrievingNode = valueRetrievingPort != null
                    ? GetPortOwnerNode(mainGraph, valueRetrievingPort)
                    : null;
                port.InitializeRuntime(mainGraph, ownerNode, valueRetrievingPort, valueRetrievingNode);
            }

            private static OerNode GetPortOwnerNode(OerMainGraph mainGraph, IOerPort port) => mainGraph.GetNode(port.NodeId);

            private static IOerPort GetPortValueRetrievingPort(OerMainGraph mainGraph, IOerPort port)
            {
                if (!mainGraph._connections.ContainsKey(port.Id)) return null;
                
                var portConnections = mainGraph._connections[port.Id];
                return portConnections != null 
                    ? mainGraph.GetPort(portConnections[0])
                    : null;
            }

            private static List<IOerDynamicPort> GetNodeDynamicPorts(OerMainGraph mainGraph, OerNode node)
            {
                collectionForAllocations_dynPorts.Clear();
                
                foreach (var inPortId in node.InDynamicPortIds)
                {
                    collectionForAllocations_dynPorts.Add(mainGraph.GetDynamicPort(inPortId));
                }

                foreach (var outPortId in node.OutDynamicPortIds)
                {
                    collectionForAllocations_dynPorts.Add(mainGraph.GetDynamicPort(outPortId));
                }

                return collectionForAllocations_dynPorts;
            }

            private static void InitializeDynamicPortRuntime(OerMainGraph mainGraph, IOerDynamicPort port)
            {
                var ownerNode = GetPortOwnerNode(mainGraph, port);
                collectionForAllocations_ports.Clear();

                foreach (var portId in port.PortIds)
                {
                    collectionForAllocations_ports.Add(mainGraph.GetPort(portId));
                }
                
                port.InitializeRuntime(mainGraph, ownerNode, collectionForAllocations_ports);
            }
            
            private static OerNode GetPortOwnerNode(OerMainGraph mainGraph, IOerDynamicPort port) => mainGraph.GetNode(port.NodeId);
        }
    }
}