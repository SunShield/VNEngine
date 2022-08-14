using System.Collections.Generic;
using VNEngine.Editor.Graphs.Systems.Connecting;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs.Systems.Building
{
    public static class NGraphBuilder
    {
        public static void BuildGraphView(NGraphView graphView)
        {
            var graphAsset = graphView.Graph;
            var runtimeGraph = graphAsset.RuntimeGraph;
            
            BuildNodes(graphView, runtimeGraph, graphAsset);
            BuildConnections(graphView, runtimeGraph);
        }

        private static void BuildNodes(NGraphView graphView, NGraph runtimeGraph, NGraphAsset graphAsset)
        {
            foreach (var nodeId in runtimeGraph.Nodes.Keys)
            {
                NNodeManager.AddExistingNode(graphAsset, graphView, nodeId);
            }
        }

        private static void BuildConnections(NGraphView graphView, NGraph runtimeGraph)
        {
            var processedPairs = new HashSet<(int a, int b)>();
            foreach (var portId in runtimeGraph.Connections.Keys)
            {
                foreach (var connectedPortId in runtimeGraph.Connections[portId].Storage)
                {
                    // if we processed (A, B) pair, we don't need to process (B, A)
                    if (processedPairs.Contains((connectedPortId, portId))) continue;
                    processedPairs.Add((portId, connectedPortId));

                    NConnectionSetupper.SetupExistingConnection(portId, connectedPortId, graphView);
                }
            }
        }
    }
}