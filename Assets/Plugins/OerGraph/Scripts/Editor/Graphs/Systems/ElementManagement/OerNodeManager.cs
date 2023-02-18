using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Unity.Data;
using OerGraph.Runtime.Unity.Data.EditorRelated;
using UnityEngine;

namespace OerGraph.Editor.Graphs.Systems.ElementManagement
{
    public static class OerNodeManager
    {
        public static void AddNewNode(OerGraphData graphData, OerGraphView graphView, Vector2 position, string nodeType)
        {
            var runtimeNode = graphData.Graph.AddNode(nodeType);
            OerNodeViewFactory.ConstructNodeView(graphView, runtimeNode, position);
            var nodeEditorData = new OerNodeEditorData() { Position = position };
            graphData.EditorData.Nodes.Add(runtimeNode.Id, nodeEditorData);
            
            OerPortManager.AddAllNodePorts(runtimeNode, graphView);
        }

        public static void AddExistingNode(OerGraphData graphData, OerGraphView graphView, int id)
        {
            var runtimeNode = graphData.Graph.GetNode(id);
            var nodeEditorData = graphData.EditorData.Nodes[id];
            OerNodeViewFactory.ConstructNodeView(graphView, runtimeNode, nodeEditorData.Position);

            OerPortManager.AddAllNodePorts(runtimeNode, graphView);
        }

        public static void RemoveNode(OerGraphData graphData, OerGraphView graphView, int nodeId)
        {
            graphData.Graph.RemoveNode(nodeId);
            graphData.EditorData.Nodes.Remove(nodeId);
            graphView.RemoveNode(nodeId);
        }
    }
}