using System.Collections.Generic;
using OerGraph.Editor.Graphs.Systems.ElementManagement;
using OerGraph.Runtime.Unity.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs.Systems.NodeMenu
{
    public class NodeMenuWindow : ScriptableObject, ISearchWindowProvider
    {
        private OerGraphView _view;
        private OerGraphAsset GraphAsset => _view.GraphAsset;

        public void SetView(OerGraphView view) => _view = view;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var result = new List<SearchTreeEntry>();
            result.Add(new SearchTreeGroupEntry(new GUIContent("Select Node"), 0));
            result.Add(new SearchTreeGroupEntry(new GUIContent("Test"), 1));
            var entry = new SearchTreeEntry(new GUIContent("TestOerNode"))
            {
                level = 2
            };
            result.Add(entry);
            return result;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var windowRoot = _view.ParentWindow.rootVisualElement;
            var windowMousePosition = _view.ParentWindow.rootVisualElement.ChangeCoordinatesTo(
                windowRoot.parent,
                context.screenMousePosition - _view.ParentWindow.position.position
            );

            var graphMousePosition = _view.contentViewContainer.WorldToLocal(windowMousePosition);
            
            OerNodeManager.AddNewNode(GraphAsset, _view, graphMousePosition, searchTreeEntry.name);
            return true;
        }
    }
}