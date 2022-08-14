using VNEngine.Runtime.Unity.Data;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class NNodeEditorDataUpdater
    {
        public static void UpdateNodeEditorData(NGraphAsset graph, NNodeView nodeView)
        {
            var editorData = graph.EditorData.Nodes[nodeView.Id];
            editorData.Position = nodeView.GetPosition().center;
        }
    }
}