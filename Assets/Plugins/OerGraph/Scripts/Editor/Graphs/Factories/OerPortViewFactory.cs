using System;
using System.Collections;
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
        private static readonly Dictionary<Type, Func<OerGraphView, IOerPort, OerPortType, OerPortView>> _portViewConstructors = new();
        private static readonly Dictionary<Type, Func<OerGraphView, IOerPort, OerPortType, OerPortView>> _dynamicPortViewConstructors = new();
        
        public static void AddPortViewMappings(Dictionary<Type, Func<OerGraphView, IOerPort, OerPortType, OerPortView>> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(IOerPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from INPort!");
                
                _portViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static void AddDynamicViewMappings(Dictionary<Type, Func<OerGraphView, IOerPort, OerPortType, OerPortView>> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(IOerPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from INPort!");
                
                _dynamicPortViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static OerPortView ConstructPort(IOerPort port, OerNodeView nodeView, OerPortType type, OerGraphView graphView, bool isDynamic = false)
        {
            var portView = !isDynamic 
                ? ConstructProperPortView(graphView, port, type) 
                : ConstructProperDynamicPortView(graphView, port, type);
            var @params = OerViewAttributeChecker.GetPortHasParamsAttribute(portView); 
            
            if (@params != null) ApplyParams(portView, @params);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        private static OerPortView ConstructProperPortView(OerGraphView graphView, IOerPort port, OerPortType type)
        {
            var runtimePortType = port.GetType();
            return _portViewConstructors.TryGetValue(runtimePortType, out var viewConstructorDelegate) 
                ? viewConstructorDelegate(graphView, port, type) 
                : new OerPortView(graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, runtimePortType);
        }
        
        private static OerPortView ConstructProperDynamicPortView(OerGraphView graphView, IOerPort port, OerPortType type)
        {
            var runtimePortType = port.GetType();
            
            // we use static custom port view for port type, if special override for dynamic ports wasn't found
            return _dynamicPortViewConstructors.TryGetValue(runtimePortType, out var dynViewConstructorDelegate) 
                ? dynViewConstructorDelegate(graphView, port, type)
                : _portViewConstructors.TryGetValue(runtimePortType, out var viewConstructorDelegate) 
                    ? viewConstructorDelegate(graphView, port, type)
                    : new OerPortView(graphView, port.Id, type == OerPortType.Input ? Direction.Input : Direction.Output, runtimePortType);
        }

        private static void ApplyParams(OerPortView portView, OerPortViewCustomParamsAttribute @params)
        {
            portView.AddToClassList(@params.ClassName);
            if (!string.IsNullOrEmpty(@params.StyleSheetPath))
                OerStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.StyleSheetPath, portView);
        }

        public static OerDynamicPortsView ConstructDynamicPortsView(IList runtimePortsList, OerNode node, OerPortType type, OerGraphView graphView)
        {
            /*var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new OerDynamicPortsView(graphView, runtimePortsList, type);
            nodeView.AddDynamicPortsView(dynamicPortsView);
            return dynamicPortsView;*/
            return null;
        }
    }
}