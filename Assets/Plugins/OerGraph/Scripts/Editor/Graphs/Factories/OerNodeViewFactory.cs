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
        private static readonly Dictionary<Type, Func<OerNode, OerNodeView>> _nodeViewConstructors = new();

        /// <summary>
        /// Funcs like (node) => new MyAwesomeNode(node) must be put there
        /// </summary>
        /// <param name="mappings"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void AddNodeViewMappings(Dictionary<Type, Func<OerNode, OerNodeView>> mappings)
        {
            foreach (var nodeType in mappings.Keys)
            {
                if (!typeof(OerNode).IsAssignableFrom(nodeType)) throw new ArgumentException($"$Type {nodeType} is not inherited from NNode!");
                
                _nodeViewConstructors.Add(nodeType, mappings[nodeType]);
            }
        }

        public static OerNodeView ConstructNodeView(OerGraphView graphView, OerNode runtimeNode, Vector2 position)
        {
            var view = ConstructProperNodeView(graphView, runtimeNode);
            var @params = OerViewAttributeChecker.GetNodeHasParamsAttribute(view);
            
            if (@params != null) ApplyParams(view, @params);
            view.SetPosition(new (position, Vector2.zero));
            graphView.AddNode(view);
            return view;
        }

        private static OerNodeView ConstructProperNodeView(OerGraphView graphView, OerNode runtimeNode)
        {
            var runtimeNodeType = runtimeNode.GetType();
            return _nodeViewConstructors.TryGetValue(runtimeNodeType, out var viewConstructorDelegate) 
                ? viewConstructorDelegate(runtimeNode)
                : new OerNodeView(graphView, runtimeNode.Id) ;
        }

        private static void ApplyParams(OerNodeView nodeView, OerNodeViewParamsAttribute @params)
        {
            nodeView.AddToClassList(@params.ClassName);
            
            if (!string.IsNullOrEmpty(@params.StyleSheetPath))
                OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.StyleSheetPath, nodeView);
        }
    }
}