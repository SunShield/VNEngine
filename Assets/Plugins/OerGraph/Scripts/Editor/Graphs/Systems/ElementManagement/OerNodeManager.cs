using OerGraph.Editor.Graphs.Factories;
using OerGraph.Runtime.Unity.Data;
using OerGraph.Runtime.Unity.Data.EditorRelated;
using UnityEngine;

namespace OerGraph.Editor.Graphs.Systems.ElementManagement
{
    public static class OerNodeManager
    {
        public static void AddNewNode(OerGraphAsset graphAsset, OerGraphView graphView, Vector2 position, string nodeType)
        {
            var runtimeNode = graphAsset.Graph.AddNode(nodeType);
            OerNodeViewFactory.ConstructNodeView(graphView, runtimeNode, position);
            var nodeEditorData = new OerNodeEditorData() { Position = position };
            graphAsset.EditorData.Nodes.Add(runtimeNode.Id, nodeEditorData);
            
            OerPortManager.AddAllNodePorts(runtimeNode, graphView);
        }

        public static void AddExistingNode(OerGraphAsset graphAsset, OerGraphView graphView, int id)
        {
            var runtimeNode = graphAsset.Graph.GetNode(id);
            var nodeEditorData = graphAsset.EditorData.Nodes[id];
            OerNodeViewFactory.ConstructNodeView(graphView, runtimeNode, nodeEditorData.Position);

            OerPortManager.AddAllNodePorts(runtimeNode, graphView);
        }

        public static void RemoveNode(OerGraphAsset graphAsset, OerGraphView graphView, int nodeId)
        {
            graphAsset.Graph.RemoveNode(nodeId);
            graphAsset.EditorData.Nodes.Remove(nodeId);
            graphView.RemoveNode(nodeId);
        }
    }
}