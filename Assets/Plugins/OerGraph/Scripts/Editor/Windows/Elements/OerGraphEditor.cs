using OerGraph.Editor.Graphs;
using OerGraph.Runtime.Unity.Data;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements
{
    public class OerGraphEditor : VisualElement
    {
        private OerGraphAsset _graph;
        
        protected OerDialogueEditorWindow ParentWindow { get; private set; }
        public OerGraphView GraphView { get; private set; }
        
        public OerGraphEditor(OerDialogueEditorWindow parentWindow)
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
            GraphView = new OerGraphView(ParentWindow);
            GraphView.StretchToParentSize();
            Add(GraphView);
        }

        private void AddGraphSaver()
        {
            //_nGraphSaver = new NGraphSaver();
        }

        public void SetGraph(OerGraphAsset graph)
        {
            // In somewhat reason, if this is not done, Edges appear as invisible on Opening graphs.
            // probably smth is not getting updated
            Remove(GraphView);
            AddGraphView();
            
            _graph = graph;
            GraphView.SetGraph(graph);
        }

        public void SaveGraph()
        {
            if (GraphView == null) return;
            
            //_nGraphSaver.SaveGraph(GraphView, _graph);
        }
    }
}