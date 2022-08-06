using UnityEngine;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Runtime.Core.Data.Factories;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class NodeManager
    {
        private static NNodeFactory _runtimeFactory = new();
        private static NNodeViewFactory _viewsFactory = new();
        
        public static void AddNewNode(NGraphAsset graphAsset, NGraphView graphView, Vector2 position)
        {
            var runtimeNode = _runtimeFactory.ConstructNode(graphAsset.RuntimeGraph);
            var nodeView = _viewsFactory.ConstructNodeView(graphView, runtimeNode, position);
            var nodeEditorData = new NNodeEditorData() { Position = position };
            graphAsset.EditorData.Nodes.Add(runtimeNode.Id, nodeEditorData);
        }

        public static void AddExistingNode(NGraphAsset graphAsset, NGraphView graphView, int id)
        {
            var runtimeNode = graphAsset.RuntimeGraph.Nodes[id];
            var editorData = graphAsset.EditorData.Nodes[id];
            var nodeView = _viewsFactory.ConstructNodeView(graphView, runtimeNode, editorData.Position);
        }

        public static void RemoveNode(NGraphAsset graphAsset, NGraphView graphView, int nodeId)
        {
            graphAsset.RuntimeGraph.RemoveNode(nodeId);
            graphAsset.EditorData.Nodes.Remove(nodeId);
            graphView.RemoveNode(nodeId);
        }

        public static void RemoveNodeView(NGraphView graphView, int nodeId)
        {
            var nodeView = graphView.Nodes[nodeId];
            graphView.RemoveNode(nodeView.Id);
        }
    }
}