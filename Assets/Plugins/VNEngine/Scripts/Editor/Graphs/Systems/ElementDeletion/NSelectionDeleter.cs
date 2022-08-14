using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Runtime.Core.Graphs.Systems.Reflection;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementDeletion
{
    public static class NSelectionDeleter
    {
        public static void DeleteSelectionElements(NGraphView graphView)
        {
            var nodesToDelete = new List<NNodeView>();
            var portsToDelete = new List<INPort>();
            var edgesToDelete = new HashSet<Edge>();

            var graphAsset = graphView.Graph;
            var runtimeGraph = graphAsset.RuntimeGraph;

            FilterSelectionElements(graphView, nodesToDelete, runtimeGraph, edgesToDelete, portsToDelete);
            DeleteEdges(graphView, edgesToDelete, runtimeGraph);
            DeletePorts(runtimeGraph, portsToDelete);
            DeleteNodes(graphView, nodesToDelete, graphAsset);
            SetGraphAssetDirty(graphAsset);
        }

        private static void FilterSelectionElements(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph,
            HashSet<Edge> edgesToDelete, List<INPort> portsToDelete)
        {
            foreach (GraphElement selectedElement in graphView.selection)
            {
                FilterSelectionElement(graphView, nodesToDelete, runtimeGraph, edgesToDelete, selectedElement, portsToDelete);
            }
        }

        private static void FilterSelectionElement(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph,
            HashSet<Edge> edgesToDelete, GraphElement selectedElement, List<INPort> portsToDelete)
        {
            if (selectedElement is NNodeView node)
            {
                ProcessNode(graphView, nodesToDelete, runtimeGraph, edgesToDelete, node, portsToDelete);
            }
            else if (selectedElement is Edge edge)
            {
                ProcessEdge(edgesToDelete, edge);
            }
        }

        private static void ProcessNode(NGraphView graphView, List<NNodeView> nodesToDelete, NGraph runtimeGraph, HashSet<Edge> edgesToDelete, NNodeView node, List<INPort> portsToDelete)
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

        private static List<INPort> GetNodePorts(NGraph runtimeGraph, NNodeView node)
        {
            var runtimeNode = runtimeGraph.Nodes[node.Id];
            var ports = NNodePortsGetter.GetNodePorts(runtimeNode);
            return ports;
        }
        
        private static void GetNodeEdges(List<INPort> nodePorts, NGraphView graphView, HashSet<Edge> edgesToDelete)
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

        private static void DeleteEdges(NGraphView graphView, HashSet<Edge> edgesToDelete, NGraph runtimeGraph)
        {
            foreach (var edge in edgesToDelete)
            {
                DeleteEdge(graphView, runtimeGraph, edge);
            }
        }

        public static void DeleteEdge(NGraphView graphView, NGraph runtimeGraph, Edge edge)
        {
            var input = edge.input as NPortView;
            var output = edge.output as NPortView;
            input.Disconnect(edge);
            output.Disconnect(edge);
            input.ConnectedEdges.Remove(edge);
            output.ConnectedEdges.Remove(edge);
            input.UpdateBackingFieldVisibility();
            output.UpdateBackingFieldVisibility();

            var inputId = (edge.input as NPortView).RuntimePort.Id;
            var outputId = (edge.output as NPortView).RuntimePort.Id;

            runtimeGraph.RemoveConnection(inputId, outputId);
            graphView.RemoveElement(edge);
        }

        private static void DeletePorts(NGraph runtimeGraph, List<INPort> portsToDelete)
        {
            foreach (var port in portsToDelete)
            {
                runtimeGraph.RemovePort(port.Id);
            }
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