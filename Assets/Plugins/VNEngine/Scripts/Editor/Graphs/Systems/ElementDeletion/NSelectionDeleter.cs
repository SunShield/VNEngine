using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementDeletion
{
    public static class NSelectionDeleter
    {
        public static void DeleteSelectionElements(NGraphView graphView)
        {
            var nodesToDelete = new List<NNodeView>();
            var edgesToDelete = new HashSet<Edge>();

            var graphAsset = graphView.Graph;
            var runtimeGraph = graphAsset.RuntimeGraph;

            FilterSelectionElements(graphView, nodesToDelete, runtimeGraph, edgesToDelete);
            DeleteEdges(graphView, edgesToDelete, runtimeGraph);
            DeleteNodes(graphView, nodesToDelete, graphAsset);
            SetGraphAssetDirty(graphAsset);
        }

        private static void FilterSelectionElements(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph,
            HashSet<Edge> edgesToDelete)
        {
            foreach (GraphElement selectedElement in graphView.selection)
            {
                FilterSelectionElement(graphView, nodesToDelete, runtimeGraph, edgesToDelete, selectedElement);
            }
        }

        private static void FilterSelectionElement(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph,
            HashSet<Edge> edgesToDelete, GraphElement selectedElement)
        {
            if (selectedElement is NNodeView node)
            {
                ProcessNode(graphView, nodesToDelete, runtimeGraph, edgesToDelete, node);
            }
            else if (selectedElement is Edge edge)
            {
                ProcessEdge(edgesToDelete, edge);
            }
        }

        private static void ProcessNode(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph, HashSet<Edge> edgesToDelete,
            NNodeView node)
        {
            nodesToDelete.Add(node);
            GetNodeEdges(graphView, runtimeGraph, edgesToDelete, node);
        }

        private static void ProcessEdge(HashSet<Edge> edgesToDelete, Edge edge)
        {
            if (edgesToDelete.Contains(edge)) return;
            edgesToDelete.Add(edge);
        }

        private static void GetNodeEdges(NGraphView graphView, NGraph runtimeGraph, HashSet<Edge> edgesToDelete, NNodeView node)
        {
            var runtimeNode = runtimeGraph.Nodes[node.Id];
            var ports = runtimeNode.GetPorts();
            foreach (var port in ports)
            {
                var portView = graphView.AllPorts[port.Id];
                foreach (var edge in portView.ConnectedEdges)
                {
                    if (edgesToDelete.Contains(edge)) continue;
                    edgesToDelete.Add(edge);
                }
            }
        }

        private static void DeleteEdges(NGraphView graphView, HashSet<Edge> edgesToDelete, NGraph runtimeGraph)
        {
            foreach (var edge in edgesToDelete)
            {
                DeleteEdge(graphView, runtimeGraph, edge);
            }
        }

        private static void DeleteEdge(NGraphView graphView, NGraph runtimeGraph, Edge edge)
        {
            var input = edge.input as NPortView;
            var output = edge.output as NPortView;
            input.Disconnect(edge);
            output.Disconnect(edge);

            var inputId = (edge.input as NPortView).RuntimePort.Id;
            var outputId = (edge.output as NPortView).RuntimePort.Id;

            runtimeGraph.RemoveConnection(inputId, outputId);
            graphView.RemoveElement(edge);
        }

        private static void DeleteNodes(NGraphView graphView, List<NNodeView> nodesToDelete, NGraphAsset graphAsset)
        {
            foreach (var node in nodesToDelete)
            {
                NNodeManager.RemoveNode(graphAsset, graphView, node.Id);
            }
        }

        private static void SetGraphAssetDirty(NGraphAsset graphAsset) => EditorUtility.SetDirty(graphAsset);
    }
}