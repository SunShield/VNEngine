using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased
{
    public partial class OerMainGraph
    {
        public static class OerGraphRuntimeInitializer
        {
            private static List<IOerPort> collectionForAllocations = new();

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
                node.InitializeRuntime(mainGraph, ports);

                foreach (var port in ports)
                {
                    InitializePortRuntime(mainGraph, port);
                }
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

            private static List<IOerPort> GetNodePorts(OerMainGraph mainGraph, OerNode node)
            {
                collectionForAllocations.Clear();
                
                foreach (var inPortId in node.InPortIds)
                {
                    collectionForAllocations.Add(mainGraph.GetPort(inPortId));
                }

                foreach (var outPortId in node.OutPortIds)
                {
                    collectionForAllocations.Add(mainGraph.GetPort(outPortId));
                }

                return collectionForAllocations;
            }

            private static OerNode GetPortOwnerNode(OerMainGraph mainGraph, IOerPort port) => mainGraph.GetNode(port.NodeId);

            private static IOerPort GetPortValueRetrievingPort(OerMainGraph mainGraph, IOerPort port)
            {
                var portConnections = mainGraph._connections[port.Id];
                return portConnections != null 
                    ? mainGraph.GetPort(portConnections[0])
                    : null;
            }
        }
    }
}