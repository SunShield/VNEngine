using System;
using System.Collections.Generic;
using UnityEngine;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Graphs.Attributes.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public static class NNodeViewFactory
    {
        private static readonly Dictionary<Type, Func<NNode, NNodeView>> _nodeViewConstructors = new();

        /// <summary>
        /// Funcs like (node) => new MyAwesomeNode(node) must be put there
        /// </summary>
        /// <param name="mappings"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void AddNodeViewMappings(Dictionary<Type, Func<NNode, NNodeView>> mappings)
        {
            foreach (var nodeType in mappings.Keys)
            {
                if (!typeof(NNode).IsAssignableFrom(nodeType)) throw new ArgumentException($"$Type {nodeType} is not inherited from NNode!");
                
                _nodeViewConstructors.Add(nodeType, mappings[nodeType]);
            }
        }

        public static NNodeView ConstructNodeView(NGraphView graphView, NNode runtimeNode, Vector2 position, NNodeParamsAttribute @params = null)
        {
            var view = ConstructProperNodeView(runtimeNode);
            if (@params != null) ApplyParams(view, @params);
            view.SetPosition(new (position, Vector2.zero));
            graphView.AddNode(view);
            return view;
        }

        private static NNodeView ConstructProperNodeView(NNode runtimeNode)
        {
            var runtimeNodeType = runtimeNode.GetType();
            return _nodeViewConstructors.TryGetValue(runtimeNodeType, out var viewConstructorDelegate) 
                ? viewConstructorDelegate(runtimeNode)
                : new NNodeView(runtimeNode) ;
        }

        private static void ApplyParams(NNodeView nodeView, NNodeParamsAttribute @params)
        {
            nodeView.AddToClassList(@params.NodeClassName);
            
            if (!string.IsNullOrEmpty(@params.NodeStyleSheetPath))
                NStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.NodeStyleSheetPath, nodeView);
        }
    }
}