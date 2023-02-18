using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Unity.Data;
using UnityEditor.Experimental.GraphView;

namespace OerGraph.Editor.Graphs.Systems.Connecting
{
    public static class OerConnectionSetupper
    {
        public static void SetupNewConnection(Edge edge, OerGraphView graphView)
        {
            var port1 = edge.input as OerPortView;
            var port2 = edge.output as OerPortView;;
            
            var port1Id = port1.RuntimePort.Id;
            var port2Id = port2.RuntimePort.Id;

            if (CheckRuntimeConnectionExists(graphView.GraphData, port1Id, port2Id)) return;
            AddRuntimeConnection(graphView.GraphData, port1Id, port2Id);

            SetupConnectionInternal(port1, port2, edge, graphView);
        }
        
        private static bool CheckRuntimeConnectionExists(OerGraphData graph, int port1Id, int port2Id) => graph.Graph.CheckConnectionExists(port1Id, port2Id);
        private static void AddRuntimeConnection(OerGraphData graph, int port1Id, int port2Id) => graph.Graph.AddConnection(port1Id, port2Id);
        
        public static void SetupExistingConnection(int port1Id, int port2Id, OerGraphView graphView)
        {
            var port1 = graphView.AllPorts[port1Id];
            var port2 = graphView.AllPorts[port2Id];
            
            var edge = OerEdgeFactory.CreateEdge(port1, port2);
            SetupConnectionInternal(port1, port2, edge, graphView);
        }
        
        private static void SetupConnectionInternal(OerPortView port1, OerPortView port2, Edge edge, OerGraphView graphView)
        {
            port1.ConnectToEdge(edge);
            port2.ConnectToEdge(edge);
            graphView.AddElement(edge);
        }
    }
}