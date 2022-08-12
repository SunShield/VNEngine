using UnityEditor;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs.Systems.Save
{
    public class NGraphSaver
    {
        public void SaveGraph(NGraphView graphView, NGraphAsset graph)
        {
            foreach (var node in graphView.Nodes.Values)
            {
                NNodeEditorDataUpdater.UpdateNodeEditorData(graph, node);
            }
            
            EditorUtility.SetDirty(graph);
        }
    }
}