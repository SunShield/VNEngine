using System.Collections.Generic;
using OerGraph.Editor.Graphs.Systems.Connecting;
using OerGraph.Editor.Graphs.Systems.ElementManagement;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;

namespace OerGraph.Editor.Graphs.Systems.Building
{
    public static class OerGraphBuilder
    {
        public static void BuildGraphView(OerGraphView graphView)
        {
            var graphAsset = graphView.GraphData;
            var runtimeGraph = graphAsset.Graph;
            
            BuildNodes(graphView, runtimeGraph, graphAsset);
            BuildConnections(graphView, runtimeGraph);
        }

        private static void BuildNodes(OerGraphView graphView, OerMainGraph runtimeGraph, OerGraphData graphData)
        {
            foreach (var nodeId in runtimeGraph.Nodes.Keys)
            {
                OerNodeManager.AddExistingNode(graphData, graphView, nodeId);
            }
        }

        private static void BuildConnections(OerGraphView graphView, OerMainGraph runtimeGraph)
        {
            var processedPairs = new HashSet<(int a, int b)>();
            foreach (var portId in runtimeGraph.Connections.Keys)
            {
                var connectedPortsIds = runtimeGraph.Connections[portId];
                foreach (var connectedPortId in connectedPortsIds.Datas)
                {
                    if (processedPairs.Contains((connectedPortId, portId))) continue;
                    processedPairs.Add((portId, connectedPortId));
                    
                    OerConnectionSetupper.SetupExistingConnection(portId, connectedPortId, graphView);
                }
            }
        }
    }
}