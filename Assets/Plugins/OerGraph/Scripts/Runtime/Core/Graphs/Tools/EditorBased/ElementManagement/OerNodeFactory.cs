using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerNodeFactory
    {
        private static readonly Dictionary<string, Type> _runtimeNodeKeys = new();
        public static Dictionary<string, Type>.KeyCollection NodeNames => _runtimeNodeKeys.Keys;

        public static void DropMappings() => _runtimeNodeKeys.Clear();

        public static void AddNodeKeyMappings(Dictionary<string, Type> nodeKeys)
        {
            foreach (var nodeKey in nodeKeys.Keys)
            {
                if (!typeof(OerNode).IsAssignableFrom(nodeKeys[nodeKey])) throw new ArgumentException($"$Type {nodeKey} is not inherited from OerNode!");

                if (_runtimeNodeKeys.ContainsKey(nodeKey)) _runtimeNodeKeys[nodeKey] = nodeKeys[nodeKey];
                else                                       _runtimeNodeKeys.Add(nodeKey, nodeKeys[nodeKey]);
            }
        }

        public static OerNode CreateNode(OerMainGraph mainGraph, string nodeKey, int id)
        {
            var node = (OerNode) Activator.CreateInstance(_runtimeNodeKeys[nodeKey]);
            node.Initialize(id);
            CreateNodePorts(mainGraph, node);
            return node;
        }
        
        private static void CreateNodePorts(OerMainGraph mainGraph, OerNode node)
        {
            foreach (var portData in node.PortsData.Ports)
            {
                if (!portData.dynamic)
                {
                    CreateNodePort(mainGraph, node, portData);
                }
                else
                {
                    CreateNodeDynamicPort(mainGraph, node, portData);
                }
            }
        }

        private static void CreateNodeDynamicPort(OerMainGraph mainGraph, OerNode node,
            (string portValueType, string portName, OerPortType portType, bool dynamic) portData)
        {
            var port = mainGraph.AddDynamicPort(portData.portValueType, portData.portName, portData.portType, node.Id);
            node.AddDynamicPort(port.Id, port.Type);
        }

        private static void CreateNodePort(OerMainGraph mainGraph, OerNode node,
            (string portValueType, string portName, OerPortType portType, bool dynamic) portData)
        {
            var port = mainGraph.AddPort(portData.portValueType, portData.portName, portData.portType, node.Id);
            node.AddPort(port.Id, port.Type);
        }
    }
}