using System;
using System.Collections.Generic;
using System.Linq;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using OerGraph.Runtime.Core.Service.Extensions;
using Unity.VisualScripting;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerNodeFactory
    {
        private static readonly Dictionary<string, Type> _runtimeNodeKeys = new();
        public static IReadOnlyDictionary<string, Type> NodeMappings => _runtimeNodeKeys;

        /// Contains data about nodes able to be spawned for specific graph only 
        private static readonly Dictionary<Type, HashSet<Type>> _nodeTypeToGraphTypeMappings = new();
        public static IReadOnlyDictionary<Type, HashSet<Type>> NodeTypeToGraphTypeMappings => _nodeTypeToGraphTypeMappings;

        public static void DropMappings()
        {
            _runtimeNodeKeys.Clear();
            _nodeTypeToGraphTypeMappings.Clear();
        }

        public static void AddNodeKeyMappings(Dictionary<string, Type> nodeKeys, HashSet<Type> graphTypes = null)
        {
            // If no graph types specified, use graph base class
            graphTypes ??= new() { typeof(OerMainGraph) };
            
            foreach (var nodeKey in nodeKeys.Keys)
            {
                if (!typeof(OerNode).IsAssignableFrom(nodeKeys[nodeKey])) throw new ArgumentException($"$Type {nodeKey} is not inherited from OerNode!");

                var nodeType = nodeKeys[nodeKey];
                if (_runtimeNodeKeys.ContainsKey(nodeKey)) _runtimeNodeKeys[nodeKey] = nodeType;
                else                                       _runtimeNodeKeys.Add(nodeKey, nodeType);
                
                // Adding all the graph base types current node can be added to
                if (!_nodeTypeToGraphTypeMappings.ContainsKey(nodeType)) _nodeTypeToGraphTypeMappings.Add(nodeType, new());
                foreach (var graphType in graphTypes)
                {
                    var graphTypeHierarchy = graphType.GetTypeHierarchy(typeof(OerMainGraph));
                    _nodeTypeToGraphTypeMappings[nodeType].UnionWith(graphTypeHierarchy);
                }
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