using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Systems.ElementsManipulation;
using VNEngine.Plugins.VNEngine.Scripts.Editor.Windows.GraphEditor;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Graphs
{
    public class NGraphView : GraphView
    {
        private NDialogueEditorWindow _editorWindow;
        
        public NGraphAsset Graph { get; private set; }
        public Dictionary<int, NNodeView> Nodes = new();

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
                    NodeManager.AddNewNode(Graph, this, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition));
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
        }

        private void ClearGraph()
        {
            var keys = Nodes.Keys.ToList();
            
            foreach (var nodeId in keys)
            {
                NodeManager.RemoveNodeView(this, nodeId);
            }
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
            Nodes.Remove(id);
            RemoveElement(node);
            
            EditorUtility.SetDirty(Graph);
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

                foreach (GraphElement selectedElement in selection)
                {
                    if (selectedElement is NNodeView node)
                    {
                        nodesToDelete.Add(node);
                    }
                }

                foreach (var node in nodesToDelete)
                {
                    NodeManager.RemoveNode(Graph, this, node.Id);
                }
            };
        }
    }
}
