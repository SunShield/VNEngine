using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph.Editor.Graphs.Systems.Building;
using OerGraph.Editor.Graphs.Systems.Connecting;
using OerGraph.Editor.Graphs.Systems.ElementManagement;
using OerGraph.Editor.Graphs.Systems.NodeMenu;
using OerGraph.Editor.Service.Utilities;
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
        private NodeMenuWindow _searchWindow;
        
        public EditorWindow ParentWindow { get; private set; }
        public OerGraphAsset Asset { get; private set; }
        public OerGraphData GraphData { get; private set; }
        public OerMainGraph Graph => GraphData.Graph;
        
        public Dictionary<int, OerNodeView> Nodes { get; } = new();
        public Dictionary<int, OerPortView> InputPorts { get; } = new();
        public Dictionary<int, OerPortView> OutputPorts { get; } = new();
        public Dictionary<int, OerPortView> AllPorts { get; } = new();

        public OerGraphView(EditorWindow parentWindow)
        {
            ParentWindow = parentWindow;
            AddToClassList(OerViewConsts.ViewClasses.OerGraphView);
            AddBackground();
            AddStyles();
            AddManipulators();
            SetDeleteElementsHandler();
            
            nodeCreationRequest = (ctx) =>
            {
                if (GraphData == null) return;
                
                SearchWindow.Open(new SearchWindowContext(ctx.screenMousePosition), _searchWindow);
            };

            _searchWindow = ScriptableObject.CreateInstance<NodeMenuWindow>();
            _searchWindow.SetView(this);
            _searchWindow.DropGroups();
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
            //this.AddManipulator(CreateAddNodeContextualMenuManipulator());
        }

        private IManipulator CreateAddNodeContextualMenuManipulator()
        {
            var cmm = new ContextualMenuManipulator(menuEvent => 
                menuEvent.menu.AppendAction("Add Node", actionEvent =>
                {
                    if (GraphData == null) return;
                    
                    /*OerNodeManager.AddNewNode(GraphAsset, this, 
                        OerEditorWindowUtilities.GetLocalMousePosition(_parentWindow, contentViewContainer, actionEvent.eventInfo.localMousePosition), "TestOerNode");*/
                    SearchWindow.Open(new SearchWindowContext(OerEditorWindowUtilities.GetLocalMousePosition(ParentWindow, contentViewContainer, actionEvent.eventInfo.localMousePosition)), _searchWindow);
                }));
            return cmm;
        }

        public void SetAsset(OerGraphAsset asset)
        {
            Asset = asset;
        }
        
        public void SetGraph(OerGraphData data)
        {
            GraphData = data;

            if (GraphData != null) BuildGraph();
        }

        private void BuildGraph() => OerGraphBuilder.BuildGraphView(this);
        
        private void SetDeleteElementsHandler()
        {
            deleteSelection = (operationName, user) => OerElementDeleter.DeleteSelectionElements(this);
        }

        public void AddNode(OerNodeView node)
        {
            Nodes.Add(node.RuntimeNodeId, node);
            AddElement(node);
            
            EditorUtility.SetDirty(Asset);
        }
        
        public void RemoveNode(int id)
        {
            var node = Nodes[id];
            foreach (var inputPort in node.Inputs.Values)
            {
                InputPorts.Remove(inputPort.RuntimePortId);
                AllPorts.Remove(inputPort.RuntimePortId);
            }

            foreach (var outputPort in node.Outputs.Values)
            {
                OutputPorts.Remove(outputPort.RuntimePortId);
                AllPorts.Remove(outputPort.RuntimePortId);
            }
            
            Nodes.Remove(id);
            RemoveElement(node);
            
            EditorUtility.SetDirty(Asset);
        }
        
        public void RegisterPort(OerPortView port)
        {
            if      (port.direction == Direction.Input)  InputPorts.Add(port.RuntimePort.Id, port);
            else if (port.direction == Direction.Output) OutputPorts.Add(port.RuntimePort.Id, port);
            AllPorts.Add(port.RuntimePort.Id, port);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) => OerCompatiblePortsGettter.GetCompatiblePorts(startPort as OerPortView, this);
    }
}