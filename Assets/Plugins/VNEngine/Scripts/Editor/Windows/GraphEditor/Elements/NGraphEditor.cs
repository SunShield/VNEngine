using UnityEngine.UIElements;
using VNEngine.Editor.Graphs;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Plugins.VNEngine.Scripts.Editor.Windows.GraphEditor.Elements
{
    public class NGraphEditor : VisualElement
    {
        private NGraphAsset _graph;
        
        protected NDialogueEditorWindow ParentWindow { get; private set; }
        public NGraphView GraphView { get; private set; }
        
        public NGraphEditor(NDialogueEditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            ConfigureRoot();
            AddGraphView();
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
            _graph = graph;
            GraphView.SetGraph(graph);
        }
    }
}