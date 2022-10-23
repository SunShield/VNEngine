using System.Collections.Generic;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports;

namespace OerGraph.Core.Data.Graph
{
    public partial class OerGraph
    {
        public static class OerGraphRuntimeInitializer
        {
            private static List<IOerPort> collectionForAllocations = new();

            public static void InitializeGraphRuntime(OerGraph graph)
            {
                foreach (var nodeId in graph._nodes.Keys)
                {
                    InitializeNodeRuntime(graph, nodeId);
                }
            }

            private static void InitializeNodeRuntime(OerGraph graph, int nodeId)
            {
                var node = graph._nodes[nodeId];
                var ports = GetNodePorts(graph, node);
                node.InitializeRuntime(graph, ports);

                foreach (var port in ports)
                {
                    InitializePortRuntime(graph, port);
                }
            }

            private static void InitializePortRuntime(OerGraph graph, IOerPort port)
            {
                var ownerNode = GetPortOwnerNode(graph, port);
                var valueRetrievingPort = GetPortValueRetrievingPort(graph, port);
                var valueRetrievingNode = valueRetrievingPort != null
                    ? GetPortOwnerNode(graph, valueRetrievingPort)
                    : null;
                port.InitializeRuntime(graph, ownerNode, valueRetrievingPort, valueRetrievingNode);
            }

            private static List<IOerPort> GetNodePorts(OerGraph graph, OerNode node)
            {
                collectionForAllocations.Clear();
                
                foreach (var inPortId in node.InPortIds)
                {
                    collectionForAllocations.Add(graph.GetPort(inPortId));
                }

                foreach (var outPortId in node.OutPortIds)
                {
                    collectionForAllocations.Add(graph.GetPort(outPortId));
                }

                return collectionForAllocations;
            }

            private static OerNode GetPortOwnerNode(OerGraph graph, IOerPort port) => graph.GetNode(port.NodeId);

            private static IOerPort GetPortValueRetrievingPort(OerGraph graph, IOerPort port)
            {
                var portConnections = graph._connections[port.Id];
                return portConnections != null 
                    ? graph.GetPort(portConnections[0])
                    : null;
            }
        }
    }
}