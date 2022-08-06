using UnityEditor;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs.Systems.Save
{
    public class GraphSaver
    {
        private NGraphView _graphView;
        
        public GraphSaver(NGraphView graphView)
        {
            _graphView = graphView;
        }

        public void SaveGraph(NGraphAsset graph)
        {
            foreach (var node in _graphView.Nodes.Values)
            {
                NodeEditorDataUpdater.UpdateNodeEditorData(graph, node);
            }
            
            EditorUtility.SetDirty(graph);
        }
    }
}