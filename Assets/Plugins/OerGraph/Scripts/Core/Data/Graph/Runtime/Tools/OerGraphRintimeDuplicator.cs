using OerGraph.Core.Service.Classes.Dicts;

namespace OerGraph.Core.Data.Graph
{
    public partial class OerGraph
    {
        public static class OerGraphRintimeDuplicator
        {
            public static OerGraph DuplicateGraph(OerGraph graph)
            {
                var newGraph = new OerGraph();
                
                // At first, we copy graph entities: nodes, ports and connections 
                CopyNodes(graph, newGraph);
                CopyPorts(graph, newGraph);
                CopyConnections(graph, newGraph);
                
                // And only then we link them
                // TODO: actually, a not very performant way of doing it. Look for better solution if performance is bad
                newGraph.InitializeRuntime();
                
                return newGraph;
            }

            private static void CopyNodes(OerGraph graph, OerGraph newGraph)
            {
                foreach (var nodeKey in graph._nodes.Keys)
                {
                    var originalNode = graph._nodes[nodeKey];
                    var copiedNode = originalNode.CreateOwnNonInitializedCopy();
                    newGraph._nodes.Add(copiedNode.Id, copiedNode);
                }
            }

            private static void CopyPorts(OerGraph graph, OerGraph newGraph)
            {
                foreach (var portKey in graph._ports.Keys)
                {
                    var originalPort = graph._ports[portKey];
                    var copiedPort = originalPort.CreateOwnNonInitializedCopy();
                    newGraph._ports.Add(copiedPort.Id, copiedPort);
                }
            }

            private static void CopyConnections(OerGraph graph, OerGraph newGraph)
            {
                foreach (var portId in graph._connections.Keys)
                {
                    var originalConnections = graph._connections[portId];
                    var copiedConnections = new IntList(originalConnections);
                    newGraph._connections.Add(portId, copiedConnections);
                }
            }
        }    
    }
}