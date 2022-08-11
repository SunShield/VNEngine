using UnityEngine.UIElements;
using VNEngine.Editor.Graphs;
using VNEngine.Editor.Graphs.Systems.Save;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Plugins.VNEngine.Scripts.Editor.Windows.GraphEditor.Elements
{
    public class NGraphEditor : VisualElement
    {
        private NGraphAsset _graph;
        private NGraphSaver _nGraphSaver;
        
        protected NDialogueEditorWindow ParentWindow { get; private set; }
        public NGraphView GraphView { get; private set; }
        
        public NGraphEditor(NDialogueEditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            ConfigureRoot();
            AddGraphView();
            AddGraphSaver();
        }

        private void ConfigureRoot()
        {
            style.flexGrow = 1;
        }
        
        private void AddGraphView()
        {
            GraphView = new NGraphView(ParentWindow);
            GraphView.StretchToParentSize();
            Add(GraphView);
        }

        public void SetGraph(NGraphAsset graph)
        {
            // In somewhat reason, if this is not done, Edges appear as invisible on Opening graphs.
            // probably smth is not getting updated
            Remove(GraphView);
            AddGraphView();
            
            _graph = graph;
            GraphView.SetGraph(graph);
        }

        private void AddGraphSaver() => _nGraphSaver = new NGraphSaver(GraphView);
        public void SaveGraph() => _nGraphSaver.SaveGraph(_graph);
    }
}