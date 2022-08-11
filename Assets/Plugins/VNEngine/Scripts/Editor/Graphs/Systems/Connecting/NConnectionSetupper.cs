using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs.Systems.Connecting
{
    public static class NConnectionSetupper
    {
        public static void SetupNewConnection(Edge edge, NGraphView graphView)
        {
            var port1 = edge.input as NPortView;
            var port2 = edge.output as NPortView;;
            
            var port1Id = port1.RuntimePort.Id;
            var port2Id = port2.RuntimePort.Id;

            if (CheckRuntimeConnectionExists(graphView.Graph, port1Id, port2Id)) return;
            AddRuntimeConnection(graphView.Graph, port1Id, port2Id);

            SetupConnectionInternal(port1, port2, edge, graphView);
        }
        
        private static bool CheckRuntimeConnectionExists(NGraphAsset graph, int port1Id, int port2Id) => graph.RuntimeGraph.CheckConnectionExists(port1Id, port2Id);
        private static void AddRuntimeConnection(NGraphAsset graph, int port1Id, int port2Id) => graph.RuntimeGraph.AddConnection(port1Id, port2Id);
        
        public static void SetupExistingConnection(int port1Id, int port2Id, NGraphView graphView)
        {
            var port1 = graphView.AllPorts[port1Id];
            var port2 = graphView.AllPorts[port2Id];
            
            var edge = NEdgeFactory.CreateEdge(port1, port2);
            SetupConnectionInternal(port1, port2, edge, graphView);
        }
        
        private static void SetupConnectionInternal(NPortView port1, NPortView port2, Edge edge, NGraphView graphView)
        {
            port1.ConnectToEdge(edge);
            port2.ConnectToEdge(edge);
            graphView.AddElement(edge);
        }
    }
}