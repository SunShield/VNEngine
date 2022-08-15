using UnityEngine;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Runtime.Core.Graphs.Attributes.Checkers;
using VNEngine.Runtime.Core.Graphs.Data.Factories;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class NNodeManager
    {
        private static NNodeFactory _runtimeFactory = new();
        
        public static void AddNewNode(NGraphAsset graphAsset, NGraphView graphView, Vector2 position, string nodeType)
        {
            var runtimeNode = _runtimeFactory.ConstructNode(graphAsset.RuntimeGraph, nodeType);
            var nodeParams = NAttributeChecker.GetNodeHasParamsAttribute(runtimeNode);
            var nodeView = NNodeViewFactory.ConstructNodeView(graphView, runtimeNode, position, nodeParams);
            var nodeEditorData = new NNodeEditorData() { Position = position };
            graphAsset.EditorData.Nodes.Add(runtimeNode.Id, nodeEditorData);
            
            NPortManager.AddAllNodePorts(runtimeNode, graphView);
        }

        public static void AddExistingNode(NGraphAsset graphAsset, NGraphView graphView, int id)
        {
            var runtimeNode = graphAsset.RuntimeGraph.Nodes[id];
            var editorData = graphAsset.EditorData.Nodes[id];
            var nodeParams = NAttributeChecker.GetNodeHasParamsAttribute(runtimeNode);
            var nodeView = NNodeViewFactory.ConstructNodeView(graphView, runtimeNode, editorData.Position, nodeParams);

            NPortManager.AddAllNodeExistingPorts(runtimeNode, nodeView, graphView);
        }

        public static void RemoveNode(NGraphAsset graphAsset, NGraphView graphView, int nodeId)
        {
            graphAsset.RuntimeGraph.RemoveNode(nodeId);
            graphAsset.EditorData.Nodes.Remove(nodeId);
            graphView.RemoveNode(nodeId);
        }
    }
}