using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph.Editor.Graphs.Systems.Building;
using OerGraph.Editor.Graphs.Systems.ElementManagement;
using OerGraph.Editor.Service.Utilities;
using OerGraph.Editor.Windows;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs
{
    public class OerGraphView : GraphView
    {
        private OerDialogueEditorWindow _parentWindow;
        
        public OerGraphAsset GraphAsset { get; private set; }
        public OerMainGraph Graph => GraphAsset.Graph;
        
        public Dictionary<int, OerNodeView> Nodes { get; } = new();
        public Dictionary<int, OerPortView> InputPorts { get; } = new();
        public Dictionary<int, OerPortView> OutputPorts { get; } = new();
        public Dictionary<int, OerPortView> AllPorts { get; } = new();

        public OerGraphView(OerDialogueEditorWindow parentWindow)
        {
            _parentWindow = parentWindow;
            AddToClassList(OerViewConsts.ViewClasses.OerGraphView);
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
            StyleSheet styles = (StyleSheet) Resources.Load("Styles/OerGraphViewStyles");
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
                    if (GraphAsset == null) return;
                    
                    OerNodeManager.AddNewNode(GraphAsset, this, 
                        OerEditorWindowUtilities.GetLocalMousePosition(_parentWindow, contentViewContainer, actionEvent.eventInfo.localMousePosition), "TestOerNode");
                }));
            return cmm;
        }
        
        public void SetGraph(OerGraphAsset graph)
        {
            GraphAsset = graph;

            if (GraphAsset != null) BuildGraph();
        }

        private void BuildGraph() => OerGraphBuilder.BuildGraphView(this);
        
        private void SetDeleteElementsHandler()
        {
            //deleteSelection = (operationName, user) => NSelectionDeleter.DeleteSelectionElements(this);
        }

        public void AddNode(OerNodeView node)
        {
            Nodes.Add(node.RuntimeNodeId, node);
            AddElement(node);
            
            EditorUtility.SetDirty(GraphAsset);
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
            
            EditorUtility.SetDirty(GraphAsset);
        }
        
        public void RegisterPort(OerPortView port)
        {
            if      (port.direction == Direction.Input)  InputPorts.Add(port.RuntimePort.Id, port);
            else if (port.direction == Direction.Output) OutputPorts.Add(port.RuntimePort.Id, port);
            AllPorts.Add(port.RuntimePort.Id, port);
        }
    }
}