using System;
using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Attributes;
using OerGraph.Editor.Graphs.Elements.Nodes;
using OerGraph.Editor.Graphs.Systems.AttributeChecking;
using OerGraph.Editor.Graphs.Systems.Styling;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEditor.Experimental.GraphView;

namespace OerGraph.Editor.Graphs.Factories
{
    public static class OerPortViewFactory
    {
        private static readonly Dictionary<Type, Type> _portViewConstructors = new();
        private static readonly Dictionary<Type, Type> _dynamicPortViewConstructors = new();

        public static void DropMappings()
        {
            _portViewConstructors.Clear();
            _dynamicPortViewConstructors.Clear();
        }

        public static void AddPortViewMappings(Dictionary<Type, Type> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(IOerPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from IOerPort!");
                if (!typeof(OerPortView).IsAssignableFrom(mappings[portType])) throw new ArgumentException($"$Type {mappings[portType]} is not inherited from OerPortView!");
                
                _portViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static void AddDynamicViewMappings(Dictionary<Type, Type> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(IOerPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from IOerDynamicPort!");
                if (!typeof(OerDynamicPortView).IsAssignableFrom(mappings[portType])) throw new ArgumentException($"$Type {mappings[portType]} is not inherited from OerDynamicPortView!");
                
                _dynamicPortViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static OerPortView ConstructRegularPort(IOerPort port, OerNodeView nodeView, OerPortType type, OerGraphView graphView)
        {
            var portView = ConstructProperPortView(graphView, port, type);
            var @params = OerViewAttributeChecker.GetPortHasParamsAttribute(portView); 
            if (@params != null) ApplyParams(portView, @params);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        public static OerDynamicPortView ConstructDynamicPort(IOerPort port, OerPortType type, OerGraphView graphView)
        {
            var portView = ConstructProperDynamicPortView(graphView, port, type);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        private static OerPortView ConstructProperPortView(OerGraphView graphView, IOerPort port, OerPortType type)
        {
            var runtimePortType = port.GetType();
            var portHasCustomView = _portViewConstructors.TryGetValue(runtimePortType, out var portViewType);
            return portHasCustomView
                ? Activator.CreateInstance(portViewType, graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, port.GetUnderlyingType()) as OerPortView
                : new OerPortView(graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, port.GetUnderlyingType());
        }
        
        private static OerDynamicPortView ConstructProperDynamicPortView(OerGraphView graphView, IOerPort port, OerPortType type)
        {
            var runtimePortType = port.GetType();
            
            var portHasCustomView = _dynamicPortViewConstructors.TryGetValue(runtimePortType, out var portViewType);
            return portHasCustomView
                ? Activator.CreateInstance(portViewType, graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, port.GetUnderlyingType()) as OerDynamicPortView
                : new OerDynamicPortView(graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, port.GetUnderlyingType());
        }

        private static void ApplyParams(OerPortView portView, OerPortViewCustomParamsAttribute @params)
        {
            portView.AddToClassList(@params.ClassName);
            if (!string.IsNullOrEmpty(@params.StyleSheetPath))
                OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.StyleSheetPath, portView);
        }

        public static OerDynamicPortsView ConstructDynamicPortsView(List<int> runtimePortsList, string name, OerNode node, OerPortType type, OerGraphView graphView)
        {
            var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new OerDynamicPortsView(graphView, name, runtimePortsList, type);
            nodeView.AddDynamicPortsView(dynamicPortsView);
            return dynamicPortsView;
        }
    }
}