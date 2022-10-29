using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.Building;
using VNEngine.Editor.Graphs.Systems.ElementDeletion;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Editor.Service.Utilities;
using VNEngine.Plugins.VNEngine.Editor.Windows.GraphEditor;
using VNEngine.Editor.Graphs.Elements.Nodes;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs
{
    public class NGraphView : GraphView
    {
        private NDialogueEditorWindow _editorWindow;
        
        public NGraphAsset Graph { get; private set; }
        public Dictionary<int, NNodeView> Nodes { get; } = new();
        public Dictionary<int, NPortView> InputPorts { get; } = new();
        public Dictionary<int, NPortView> OutputPorts { get; } = new();
        public Dictionary<int, NPortView> AllPorts { get; } = new();

        public NGraphView(NDialogueEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            AddToClassList("n-graphView");
            AddBackground();
            AddStyles();
            AddManipulators();
            SetDeleteElementsHandler();
        }

        private void AddBackground()
        {
            var bg = new GridBackground();
            bg.StretchToParentSize();
            Insert(0, bg);
        }

        private void AddStyles()
        {
            StyleSheet styles = (StyleSheet) Resources.Load("Styles/NGraphViewStyles");
            styleSheets.Add(styles);
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateAddNodeContextualMenuManipulator());
        }

        private IManipulator CreateAddNodeContextualMenuManipulator()
        {
            var cmm = new ContextualMenuManipulator(menuEvent => 
                menuEvent.menu.AppendAction("Add Test Node", actionEvent =>
                {
                    if (Graph == null) return;
                    
                    NNodeManager.AddNewNode(Graph, this, 
                        NEditorWindowUtilities.GetLocalMousePosition(_editorWindow, contentViewContainer, actionEvent.eventInfo.localMousePosition), "BasicNode");
                }));
            return cmm;
        }
        
        public void SetGraph(NGraphAsset graph)
        {
            Graph = graph;

            if (Graph != null) BuildGraph();
        }

        private void BuildGraph() => NGraphBuilder.BuildGraphView(this);

        public void AddNode(NNodeView node)
        {
            Nodes.Add(node.Id, node);
            AddElement(node);
            
            EditorUtility.SetDirty(Graph);
        }
        
        public void RemoveNode(int id)
        {
            var node = Nodes[id];
            foreach (var inputPort in node.Inputs.Values)
            {
                InputPorts.Remove(inputPort.RuntimePort.Id);
                AllPorts.Remove(inputPort.RuntimePort.Id);
            }

            foreach (var outputPort in node.Outputs.Values)
            {
                OutputPorts.Remove(outputPort.RuntimePort.Id);
                AllPorts.Remove(outputPort.RuntimePort.Id);
            }
            
            Nodes.Remove(id);
            RemoveElement(node);
            
            EditorUtility.SetDirty(Graph);
        }

        public void RegisterPort(NPortView port)
        {
            if      (port.direction == Direction.Input)  InputPorts.Add(port.RuntimePort.Id, port);
            else if (port.direction == Direction.Output) OutputPorts.Add(port.RuntimePort.Id, port);
            AllPorts.Add(port.RuntimePort.Id, port);
        }

        private void SetDeleteElementsHandler()
        {
            deleteSelection = (operationName, user) => NSelectionDeleter.DeleteSelectionElements(this);
        }
    }
}
