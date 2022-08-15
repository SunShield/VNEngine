using System;
using System.Collections;
using System.Collections.Generic;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Systems.Styling;
using VNEngine.Runtime.Core.Graphs.Attributes.Ports;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Editor.Graphs.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Factories
{
    public static class NPortViewFactory
    {
        private static readonly Dictionary<Type, Func<NGraphView, string, INPort, NPortType, NPortView>> _portViewConstructors = new();
        private static readonly Dictionary<Type, Func<NGraphView, string, INPort, NPortType, NPortView>> _dynamicPortViewConstructors = new();
        
        public static void AddPortViewMappings(Dictionary<Type, Func<NGraphView, string, INPort, NPortType, NPortView>> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(INPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from INPort!");
                
                _portViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static void AddDynamicViewMappings(Dictionary<Type, Func<NGraphView, string, INPort, NPortType, NPortView>> mappings)
        {
            foreach (var portType in mappings.Keys)
            {
                if (!typeof(INPort).IsAssignableFrom(portType)) throw new ArgumentException($"$Type {portType} is not inherited from INPort!");
                
                _dynamicPortViewConstructors.Add(portType, mappings[portType]);
            }
        }
        
        public static NPortView ConstructPort(INPort port, string fieldName, NNodeView nodeView, NPortType type, NGraphView graphView, NPortParamsAttribute @params = null, bool isDynamic = false)
        {
            var portView = !isDynamic 
                ? ConstructProperPortView(graphView, fieldName, port, type) 
                : ConstructProperDynamicPortView(graphView, fieldName, port, type);
            if (@params != null) ApplyParams(portView, @params);
            nodeView.AddPort(portView);
            graphView.RegisterPort(portView);
            return portView;
        }
        
        private static NPortView ConstructProperPortView(NGraphView graphView, string fieldName, INPort port, NPortType type)
        {
            var runtimePortType = port.GetType();
            return _portViewConstructors.TryGetValue(runtimePortType, out var viewConstructorDelegate) 
                ? viewConstructorDelegate(graphView, fieldName, port, type) 
                : new NPortView(graphView, fieldName, port, type);
        }
        
        private static NPortView ConstructProperDynamicPortView(NGraphView graphView, string fieldName, INPort port, NPortType type)
        {
            var runtimePortType = port.GetType();
            
            // we use static custom port view for port type, if special override for dynamic ports wasn't found
            return _dynamicPortViewConstructors.TryGetValue(runtimePortType, out var dynViewConstructorDelegate) 
                ? dynViewConstructorDelegate(graphView, fieldName, port, type)
                : _portViewConstructors.TryGetValue(runtimePortType, out var viewConstructorDelegate) 
                    ? viewConstructorDelegate(graphView, fieldName, port, type)
                    : new NPortView(graphView, fieldName, port, type);
        }

        private static void ApplyParams(NPortView portView, NPortParamsAttribute @params)
        {
            portView.AddToClassList(@params.PortClassName);
            if (!string.IsNullOrEmpty(@params.PortStyleSheetPath))
                NStyleSheetResourceLoader.TryAddStyleSheetFromPath(@params.PortStyleSheetPath, portView);
        }

        public static NDynamicPortsView ConstructDynamicPortsView(IList runtimePortsList, NNode node, NPortType type, NGraphView graphView)
        {
            var nodeView = graphView.Nodes[node.Id];
            var dynamicPortsView = new NDynamicPortsView(graphView, runtimePortsList, type);
            nodeView.AddDynamicPortsView(dynamicPortsView);
            return dynamicPortsView;
        }
    }
}