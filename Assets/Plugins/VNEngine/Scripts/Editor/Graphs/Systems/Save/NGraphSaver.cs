using UnityEditor;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs.Systems.Save
{
    public class NGraphSaver
    {
        private NGraphView _graphView;
        
        public NGraphSaver(NGraphView graphView)
        {
            _graphView = graphView;
        }

        public void SaveGraph(NGraphAsset graph)
        {
            foreach (var node in _graphView.Nodes.Values)
            {
                NNodeEditorDataUpdater.UpdateNodeEditorData(graph, node);
            }
            
            EditorUtility.SetDirty(graph);
        }
    }
}