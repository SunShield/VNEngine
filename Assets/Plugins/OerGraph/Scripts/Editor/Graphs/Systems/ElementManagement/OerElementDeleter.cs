using System.Collections.Generic;
using System.Linq;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace OerGraph.Editor.Graphs.Systems.ElementManagement
{
    public static class OerElementDeleter
    {
        public static void DeleteSelectionElements(OerGraphView graphView)
        {
            var nodesToDelete = new List<OerNodeView>();
            var portsToDelete = new List<IOerPort>();
            var edgesToDelete = new HashSet<Edge>();

            var graphAsset = graphView.GraphData;
            var runtimeGraph = graphAsset.Graph;

            FilterSelectionElements(graphView, nodesToDelete, runtimeGraph, edgesToDelete, portsToDelete);
            DeleteEdges(graphView, edgesToDelete);
            DeleteNodes(graphView, nodesToDelete, graphAsset);
            SetGraphAssetDirty(graphView.Asset);
        }

        private static void FilterSelectionElements(OerGraphView graphView, List<OerNodeView> nodesToDelete, OerMainGraph runtimeGraph,
            HashSet<Edge> edgesToDelete, List<IOerPort> portsToDelete)
        {
            foreach (GraphElement selectedElement in graphView.selection)
            {
                FilterSelectionElement(graphView, nodesToDelete, runtimeGraph, edgesToDelete, selectedElement, portsToDelete);
            }
        }

        private static void FilterSelectionElement(OerGraphView graphView, List<OerNodeView> nodesToDelete, OerMainGraph runtimeGraph,
            HashSet<Edge> edgesToDelete, GraphElement selectedElement, List<IOerPort> portsToDelete)
        {
            if (selectedElement is OerNodeView node)
            {
                ProcessNode(graphView, nodesToDelete, runtimeGraph, edgesToDelete, node, portsToDelete);
            }
            else if (selectedElement is Edge edge)
            {
                ProcessEdge(edgesToDelete, edge);
            }
        }

        private static void ProcessNode(OerGraphView graphView, List<OerNodeView> nodesToDelete, OerMainGraph runtimeGraph, HashSet<Edge> edgesToDelete, OerNodeView node, List<IOerPort> portsToDelete)
        {
            nodesToDelete.Add(node);
            portsToDelete.AddRange(GetNodePorts(runtimeGraph, node));
            GetNodeEdges(portsToDelete, graphView, edgesToDelete);
        }

        private static void ProcessEdge(HashSet<Edge> edgesToDelete, Edge edge)
        {
            if (edgesToDelete.Contains(edge)) return;
            edgesToDelete.Add(edge);
        }

        private static List<IOerPort> GetNodePorts(OerMainGraph runtimeGraph, OerNodeView node)
        {
            List<IOerPort> ports = new();
            ports.AddRange(node.Inputs.Keys.Select(runtimeGraph.GetPort).ToList());
            ports.AddRange(node.Outputs.Keys.Select(runtimeGraph.GetPort).ToList());
            return ports;
        }
        
        private static void GetNodeEdges(List<IOerPort> nodePorts, OerGraphView graphView, HashSet<Edge> edgesToDelete)
        {
            foreach (var port in nodePorts)
            {
                var portView = graphView.AllPorts[port.Id];
                foreach (var edge in portView.ConnectedEdges)
                {
                    if (edgesToDelete.Contains(edge)) continue;
                    edgesToDelete.Add(edge);
                }
            }
        }

        private static void DeleteEdges(OerGraphView graphView, HashSet<Edge> edgesToDelete)
        {
            foreach (var edge in edgesToDelete)
            {
                DeleteEdge(graphView, edge);
            }
        }

        public static void DeleteEdge(OerGraphView graphView,  Edge edge)
        {
            var input = edge.input as OerPortView;
            var output = edge.output as OerPortView;
            input.Disconnect(edge);
            output.Disconnect(edge);
            input.ConnectedEdges.Remove(edge);
            output.ConnectedEdges.Remove(edge);
            input.UpdateBackingFieldVisibility();
            output.UpdateBackingFieldVisibility();

            /*var inputId = (edge.input as OerPortView).RuntimePort.Id;
            var outputId = (edge.output as OerPortView).RuntimePort.Id;
            runtimeGraph.RemoveConnection(inputId, outputId);*/
            
            graphView.RemoveElement(edge);
        }

        private static void DeleteNodes(OerGraphView graphView, List<OerNodeView> nodesToDelete, OerGraphData graphData)
        {
            foreach (var node in nodesToDelete)
            {
                OerNodeManager.RemoveNode(graphData, graphView, node.RuntimeNodeId);
            }
        }

        private static void SetGraphAssetDirty(OerGraphAsset graphAsset) => EditorUtility.SetDirty(graphAsset);
    }
}