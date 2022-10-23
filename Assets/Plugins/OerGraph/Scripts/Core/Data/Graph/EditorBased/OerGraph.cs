using System;
using System.Collections.Generic;
using OerGraph.Core.Service.Classes.Dicts;
using UnityEngine;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes.Creation;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports.Creation;

namespace OerGraph.Core.Data.Graph
{
    [Serializable]
    public partial class OerGraph
    {
        [SerializeField] [HideInInspector] private int _currentNodeId;
        [SerializeField] [HideInInspector] private int _currentPortId;

        [SerializeReference] [HideInInspector] private IntToOerNodeDictionary _nodes;
        [SerializeReference] [HideInInspector] private IntToOerPortDictionary _ports;
        [SerializeReference] [HideInInspector] private IntToIntListDictionary _connections;

        public OerNode AddNode(string key)
        {
            var node = OerGraphNodeCreator.CreateNode(this, key, _currentNodeId++);
            _nodes.Add(node.Id, node);
            return node;
        }

        public void RemoveNode(int id)
        {
            var node = _nodes[id];
            
            var inPortIds = node.InPortIds;
            foreach (var portId in inPortIds)
                RemovePort(portId);
            
            var outPortIds = node.InPortIds;
            foreach (var portId in outPortIds)
                RemovePort(portId);
            
            _nodes.Remove(id);
        }

        public OerNode GetNode(int id) => _nodes[id];

        public IOerPort AddPort(string key, string name, OerPortType portType, int nodeId)
        {
            var port = OerPortCreator.CreatePort(key, _currentPortId++, name, portType, nodeId);
            _ports.Add(port.Id, port);
            return port;
        }

        public void RemovePort(int id)
        {
            var port = _ports[id];
            var portId = port.Id;
            var connections = new IntList(_connections[portId]);
            foreach (var connectedPortId in connections.Datas)
            {
                RemoveConnection(portId, connectedPortId);
            }
            
            _ports.Remove(id);
        }

        public IOerPort GetPort(int id) => _ports[id];

        public void AddConnection(int port1Id, int port2Id)
        {
            if (_connections.ContainsKey(port1Id)) _connections.Add(port1Id, new());
            _connections[port1Id].Add(port2Id);
            
            if (_connections.ContainsKey(port2Id)) _connections.Add(port2Id, new());
            _connections[port2Id].Add(port1Id);
        }

        public void RemoveConnection(int port1Id, int port2Id)
        {
            _connections[port1Id].Remove(port2Id);
            if (_connections[port1Id].Count == 0) _connections.Remove(port1Id);
            
            _connections[port2Id].Remove(port1Id);
            if (_connections[port2Id].Count == 0) _connections.Remove(port2Id);
        }

        public List<int> GetConnectedPortIds(int portId) => _connections[portId].Datas;
    }
}