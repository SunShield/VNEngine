using OerGraph.Runtime.Core.Service.Classes.Dicts;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased
{
    public partial class OerMainGraph
    {
        public static class OerGraphRuntimeDuplicator
        {
            public static OerMainGraph DuplicateGraph(OerMainGraph mainGraph)
            {
                var newGraph = mainGraph.CreateInstance();
                
                // At first, we copy graph entities: nodes, ports and connections 
                CopyNodes(mainGraph, newGraph);
                CopyPorts(mainGraph, newGraph);
                CopyConnections(mainGraph, newGraph);
                CopyDynamicPorts(mainGraph, newGraph);
                
                // And only then we link them
                // TODO: actually, a not very performant way of doing it. Look for better solution if performance is bad
                newGraph.InitializeRuntime();
                
                return newGraph;
            }

            private static void CopyNodes(OerMainGraph mainGraph, OerMainGraph newMainGraph)
            {
                foreach (var nodeKey in mainGraph._nodes.Keys)
                {
                    var originalNode = mainGraph._nodes[nodeKey];
                    var copiedNode = originalNode.CreateOwnNonInitializedCopy();
                    newMainGraph._nodes.Add(copiedNode.Id, copiedNode);
                }
            }

            private static void CopyPorts(OerMainGraph mainGraph, OerMainGraph newMainGraph)
            {
                foreach (var portKey in mainGraph._ports.Keys)
                {
                    var originalPort = mainGraph._ports[portKey];
                    var copiedPort = originalPort.CreateOwnNonInitializedCopy();
                    newMainGraph._ports.Add(copiedPort.Id, copiedPort);
                }
            }

            private static void CopyConnections(OerMainGraph mainGraph, OerMainGraph newMainGraph)
            {
                foreach (var portId in mainGraph._connections.Keys)
                {
                    var originalConnections = mainGraph._connections[portId];
                    var copiedConnections = new IntList(originalConnections);
                    newMainGraph._connections.Add(portId, copiedConnections);
                }
            }

            private static void CopyDynamicPorts(OerMainGraph mainGraph, OerMainGraph newMainGraph)
            {
                foreach (var dynPortId in mainGraph._dynPorts.Keys)
                {
                    var originalDynPort = mainGraph._dynPorts[dynPortId];
                    var copiedPort = originalDynPort.CreateOwnNonInitializedCopy();
                    newMainGraph._dynPorts.Add(copiedPort.Id, copiedPort);
                }
            }
        }    
    }
}