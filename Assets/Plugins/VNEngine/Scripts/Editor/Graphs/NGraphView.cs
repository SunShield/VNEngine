using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Editor.Graphs.Systems.PortCompatibility;
using VNEngine.Plugins.VNEngine.Scripts.Editor.Windows.GraphEditor;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Scripts.Editor.Service.Extensions;

namespace VNEngine.Editor.Graphs
{
    public class NGraphView : GraphView
    {
        private NDialogueEditorWindow _editorWindow;

        private PortCompatibilityChecker _portCompatibilityChecker = new();
        
        public NGraphAsset Graph { get; private set; }
        public Dictionary<int, NNodeView> Nodes { get; } = new();
        public Dictionary<int, NPortView> InputPorts { get; } = new();
        public Dictionary<int, NPortView> OutputPorts { get; } = new();
        public Dictionary<int, NPortView> AllPorts { get; } = new();

        public NGraphView(NDialogueEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
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
                    NodeManager.AddNewNode(Graph, this, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition), "BasicNode");
                }));
            return cmm;
        }
        
        public void SetGraph(NGraphAsset graph)
        {
            if (Graph != null) ClearGraph();
            
            Graph = graph;

            if (Graph != null) BuildGraph();
        }

        private void BuildGraph()
        {
            foreach (var nodeId in Graph.RuntimeGraph.Nodes.Keys)
            {
                NodeManager.AddExistingNode(Graph, this, nodeId);
            }

            var processedPairs = new HashSet<(int a, int b)>();
            foreach (var portId in Graph.RuntimeGraph.Connections.Keys)
            {
                foreach (var connectedPortId in Graph.RuntimeGraph.Connections[portId].Storage)
                {
                    // if we processed (A, B) pair, we don't need to process (B, A)
                    // TODO: think on using a unique pairs storage just for serialization?
                    if (processedPairs.Contains((connectedPortId, portId))) continue;
                    processedPairs.Add((portId, connectedPortId));

                    SetupExistingConnection(portId, connectedPortId);
                }
            }

            var t = this;
        }

        private void ClearGraph()
        {
            var keys = Nodes.Keys.ToList();
            
            foreach (var nodeId in keys)
            {
                NodeManager.RemoveNodeView(this, nodeId);
            }

            foreach (var edge in edges)
            {
                edge.parent.RemoveFromHierarchy();
            }
            
            Nodes.Clear();
            InputPorts.Clear();
            OutputPorts.Clear();
            AllPorts.Clear();
        }

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

        public void AddPort(NPortView port)
        {
            if      (port.direction == Direction.Input)  InputPorts.Add(port.RuntimePort.Id, port);
            else if (port.direction == Direction.Output) OutputPorts.Add(port.RuntimePort.Id, port);
            AllPorts.Add(port.RuntimePort.Id, port);
        }
        
        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo(_editorWindow.rootVisualElement.parent, mousePosition - _editorWindow.position.position);
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }

        private void SetDeleteElementsHandler()
        {
            deleteSelection = (operationName, user) =>
            {
                var nodesToDelete = new List<NNodeView>();
                var edgesToDelete = new HashSet<Edge>();

                foreach (GraphElement selectedElement in selection)
                {
                    if (selectedElement is NNodeView node)
                    {
                        nodesToDelete.Add(node);
                        var runtimeNode = Graph.RuntimeGraph.Nodes[node.Id];
                        var ports = runtimeNode.GetPorts();
                        foreach (var port in ports)
                        {
                            var portView = AllPorts[port.Id];
                            foreach (var edge in portView.ConnectedEdges)
                            {
                                if (edgesToDelete.Contains(edge)) continue;
                                edgesToDelete.Add(edge);
                            }
                        }
                    }
                    else if (selectedElement is Edge edge)
                    {
                        if (edgesToDelete.Contains(edge)) continue;
                        edgesToDelete.Add(edge);
                    }
                }

                foreach (var edge in edgesToDelete)
                {
                    var input = edge.input as NPortView;
                    var output = edge.output as NPortView;
                    input.Disconnect(edge);
                    output.Disconnect(edge);
                    
                    var inputId = (edge.input as NPortView).RuntimePort.Id;
                    var outputId = (edge.output as NPortView).RuntimePort.Id;

                    Graph.RuntimeGraph.RemoveConnection(inputId, outputId);
                    
                    RemoveElement(edge);
                }

                foreach (var node in nodesToDelete)
                {
                    NodeManager.RemoveNode(Graph, this, node.Id);
                }
                
                EditorUtility.SetDirty(Graph);
            };
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) => _portCompatibilityChecker.GetCompatiblePorts(startPort as NPortView, this);

        public void SetupConnection(NPortView input, NPortView output, Edge edge)
        {
            var inputId = input.RuntimePort.Id;
            var outputId = output.RuntimePort.Id;
            
            if (Graph.RuntimeGraph.Connections.ContainsKey(inputId) &&
                Graph.RuntimeGraph.Connections[inputId].Storage.Contains(outputId)) return;
            
            input.Connect(edge);
            output.Connect(edge);
            Graph.RuntimeGraph.AddConnection(input.RuntimePort.Id, output.RuntimePort.Id);
        }

        public void SetupExistingConnection(int port1Id, int port2Id)
        {
            var port1 = AllPorts[port1Id];
            var port2 = AllPorts[port2Id];
            var edge = port1.ConnectTo(port2);
            AddElement(edge);
            port1.ConnectedEdges.Add(edge);
            port2.ConnectedEdges.Add(edge);
        }
    }
}
