using OerGraph.Editor.Graphs;
using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements
{
    public class OerGraphEditor : VisualElement
    {
        private OerGraphAsset _asset;
        private OerGraphData _data;
        
        protected EditorWindow ParentWindow { get; private set; }
        public OerGraphView GraphView { get; private set; }
        
        public OerGraphEditor(EditorWindow parentWindow)
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

        public void SetAsset(OerGraphAsset asset)
        {
            Remove(GraphView);
            AddGraphView();
            _asset = asset;
            _data = null;
            
            GraphView.SetAsset(_asset);
        }

        public void SetGraph(OerGraphData data)
        {
            // In somewhat reason, if this is not done, Edges appear as invisible on Opening graphs.
            // probably smth is not getting updated
            Remove(GraphView);
            AddGraphView();
            GraphView.SetAsset(_asset);
            
            _data = data;
            GraphView.SetGraph(data);
        }

        public void SaveGraph()
        {
            if (GraphView == null) return;
            
            //_nGraphSaver.SaveGraph(GraphView, _graph);
        }
    }
}