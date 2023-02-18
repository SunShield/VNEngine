using System.Collections.Generic;
using OerGraph.Editor.Graphs.Systems.ElementManagement;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement;
using OerGraph.Runtime.Unity.Data;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Graphs.Systems.NodeMenu
{
    public class NodeMenuWindow : ScriptableObject, ISearchWindowProvider
    {
        private Texture folderIcon;
        
        private OerGraphView _view;
        private OerGraphData GraphData => _view.GraphData;

        private NodeMenuWindowTreeGroup _rootGroup = new("A");
        private bool _rootGroupsBuilt;

        public void SetView(OerGraphView view)
        {
            _view = view;
            folderIcon = EditorResources.Load<Texture>("d_Folder Icon");
        }

        public void DropGroups()
        {
            _rootGroup = new("A");
            _rootGroupsBuilt = false;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            BuildGroupsIfNeeded();
            
            var result = new List<SearchTreeEntry>();
            result.Add(new SearchTreeGroupEntry(new GUIContent("Select Node"), 0));
            DrawGroup(_rootGroup, ref result, 0);
            
            return result;
        }

        private void BuildGroupsIfNeeded()
        {
            if (_rootGroupsBuilt) return;
            
            var nodeNames = OerNodeFactory.NodeMappings.Keys;
            foreach (var nodeName in nodeNames)
            {
                if (!CheckNodeCanBePlacedOnCurrentGraph(nodeName)) continue;

                ProcessNodeName(nodeName);
            }

            _rootGroupsBuilt = true;
        }

        private bool CheckNodeCanBePlacedOnCurrentGraph(string nodeName)
        {
            var nodeType = OerNodeFactory.NodeMappings[nodeName];
            var graphTypes = OerNodeFactory.NodeTypeToGraphTypeMappings[nodeType];
            var currentGraphType = _view.Graph.GetType();

            var nodeIsValid = false;
            foreach (var graphType in graphTypes)
            {
                if (currentGraphType == graphType || currentGraphType.IsSubclassOf(graphType))
                {
                    nodeIsValid = true;
                    break;
                }
            }

            return nodeIsValid;
        }

        private void ProcessNodeName(string fullNodeName)
        {
            var nodeNameElements = fullNodeName.Split('/');
            var nodeName = nodeNameElements[^1];
            var currentGroup = _rootGroup;
            // all the node name elements except the last one are the groups 
            for (int i = 0; i < nodeNameElements.Length - 1; i++)
            {
                var groupName = nodeNameElements[i];
                if (!currentGroup.Groups.ContainsKey(groupName))
                {
                    var group = new NodeMenuWindowTreeGroup()
                    {
                        Name = groupName
                    };
                    currentGroup.Groups.Add(groupName, group);
                }
                currentGroup = currentGroup.Groups[groupName];
            }

            if (currentGroup == _rootGroup)
            {
                Debug.LogError($"Element [{nodeName}] has no group name specified!");
                return;
            }

            currentGroup.Entries.Add((nodeName, fullNodeName));
        }

        private void DrawGroup(NodeMenuWindowTreeGroup group, ref List<SearchTreeEntry> entries, int depth = 0)
        {
            if (group != _rootGroup)
            {
                var parentGroupEntry = new SearchTreeGroupEntry(new GUIContent(group.Name, folderIcon), depth);
                entries.Add(parentGroupEntry);
            }
            
            foreach (var nodeEntryData in group.Entries)
            {
                var nodeEntry = new SearchTreeEntry(new GUIContent(nodeEntryData.Item1))
                {
                    level = depth + 1,
                    userData = nodeEntryData.Item2
                };
                entries.Add(nodeEntry);
            }
            
            foreach (var treeGroup in group.Groups)
            {
                DrawGroup(treeGroup.Value, ref entries, depth + 1);
            }
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var windowRoot = _view.ParentWindow.rootVisualElement;
            var windowMousePosition = _view.ParentWindow.rootVisualElement.ChangeCoordinatesTo(
                windowRoot.parent,
                context.screenMousePosition - _view.ParentWindow.position.position
            );

            var graphMousePosition = _view.contentViewContainer.WorldToLocal(windowMousePosition);
            
            OerNodeManager.AddNewNode(GraphData, _view, graphMousePosition, (string)searchTreeEntry.userData);
            return true;
        }
    }
}