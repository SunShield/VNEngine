using System;
using System.Collections.Generic;
using OerGraph.Editor.Graphs.Attributes;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph.Editor.Graphs.Systems.AttributeChecking;
using OerGraph.Editor.Graphs.Systems.Styling;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using UnityEngine;

namespace OerGraph.Editor.Graphs.Factories
{
    public static class OerNodeViewFactory
    {
        private static readonly Dictionary<Type, Type> _runtimeNodesToTypesMap = new();

        public static void DropMappings() => _runtimeNodesToTypesMap.Clear(); 

        public static void AddNodeViewMappings(Dictionary<Type, Type> mappings)
        {
            foreach (var nodeType in mappings.Keys)
            {
                if (!typeof(OerNode).IsAssignableFrom(nodeType)) throw new ArgumentException($"$Type {nodeType} is not inherited from OerNode!");
                
                if (_runtimeNodesToTypesMap.ContainsKey(nodeType)) _runtimeNodesToTypesMap[nodeType] = mappings[nodeType];
                else                                               _runtimeNodesToTypesMap.Add(nodeType, mappings[nodeType]);
            }
        }

        public static OerNodeView ConstructNodeView(OerGraphView graphView, OerNode runtimeNode, Vector2 position)
        {
            var view = ConstructProperNodeView(graphView, runtimeNode);
            var @params = OerViewAttributeChecker.GetNodeHasParamsAttribute(view);
            
            if (@params != null) ApplyParams(view, @params);
            view.SetPosition(new (position, Vector2.zero));
            view.AdjustPortSizes();
            graphView.AddNode(view);
            return view;
        }

        private static OerNodeView ConstructProperNodeView(OerGraphView graphView, OerNode runtimeNode)
        {
            var runtimeNodeType = runtimeNode.GetType();
            var nodeHasCustomView = _runtimeNodesToTypesMap.TryGetValue(runtimeNodeType, out var customViewType);
            return nodeHasCustomView 
                ? Activator.CreateInstance(customViewType, graphView, runtimeNode.Id) as OerNodeView 
                : new OerNodeView(graphView, runtimeNode.Id);
        }

        private static void ApplyParams(OerNodeView nodeView, OerNodeViewParamsAttribute @params)
        {
            nodeView.AddToClassList(@params.ClassName);
            
            if (!string.IsNullOrEmpty(@params.StyleSheetPath))
                OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.StyleSheetPath, nodeView);
        }
    }
}