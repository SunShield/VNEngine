using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerPortFactory
    {
        private static readonly Dictionary<string, Type> _runtimePortKeys = new();
        public static Dictionary<string, Type>.KeyCollection PortNames => _runtimePortKeys.Keys;
        
        private static readonly Dictionary<string, Type> _runtimeDynamicPortKeys = new();
        public static Dictionary<string, Type>.KeyCollection DynamicPortNames => _runtimeDynamicPortKeys.Keys;
        
        public static void DropMappings()
        {
            _runtimePortKeys.Clear();
            _runtimeDynamicPortKeys.Clear();
        }

        public static void AddPortKeyMappings(Dictionary<string, Type> portKeys)
        {
            foreach (var portKey in portKeys.Keys)
            {
                if (!typeof(IOerPort).IsAssignableFrom(portKeys[portKey])) throw new ArgumentException($"$Type {portKey} is not inherited from OerNode!");

                if (_runtimePortKeys.ContainsKey(portKey)) _runtimePortKeys[portKey] = portKeys[portKey];
                else                                       _runtimePortKeys.Add(portKey, portKeys[portKey]);
            }
        }
        
        public static void AddDynamicPortKeyMappings(Dictionary<string, Type> dynamicPortKeys)
        {
            foreach (var portKey in dynamicPortKeys.Keys)
            {
                if (!typeof(IOerDynamicPort).IsAssignableFrom(dynamicPortKeys[portKey])) throw new ArgumentException($"$Type {portKey} is not inherited from OerNode!");
                
                if (_runtimeDynamicPortKeys.ContainsKey(portKey)) _runtimeDynamicPortKeys[portKey] = dynamicPortKeys[portKey];
                else                                              _runtimeDynamicPortKeys.Add(portKey, dynamicPortKeys[portKey]);
            }
        }
        
        public static IOerPort CreatePort(string portKey, int id, string name, OerPortType type, int nodeId)
        {
            var port = (IOerPort)Activator.CreateInstance(_runtimePortKeys[portKey]);
            port.Initialize(id, type, name, nodeId);
            return port;
        }

        public static IOerDynamicPort CreateDynamicPort(string portKey, int id, string name, OerPortType type, int nodeId)
        {
            var port = (IOerDynamicPort)Activator.CreateInstance(_runtimeDynamicPortKeys[portKey]);
            port.Initialize(id, type, name, nodeId);
            return port;
        }
    }
}