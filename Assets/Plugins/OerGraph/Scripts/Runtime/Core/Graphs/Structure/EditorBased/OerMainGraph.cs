using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEngine;
using OerGraph.Runtime.Core.Service.Classes.Dicts;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased
{
    [Serializable]
    public partial class OerMainGraph
    {
        [SerializeField] [HideInInspector] private int _currentNodeId;
        [SerializeField] [HideInInspector] private int _currentPortId;

        [SerializeReference] /*[HideInInspector]*/ private IntToOerNodeDictionary _nodes = new();
        [SerializeReference] /*[HideInInspector]*/ private IntToOerPortDictionary _ports = new();
        [SerializeReference] /*[HideInInspector]*/ private IntToIntListDictionary _connections = new();

        public IReadOnlyDictionary<int, OerNode> Nodes => _nodes;
        public IReadOnlyDictionary<int, IntList> Connections => _connections;

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
            
            var outPortIds = node.OutPortIds;
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

            // if we delete ports/connections in bulk, we easily can clear port connections before actual port removal 
            if (_connections.ContainsKey(portId))
            {
                var connections = new IntList(_connections[portId]);
                foreach (var connectedPortId in connections.Datas)
                {
                    RemoveConnection(portId, connectedPortId);
                }
            }
            
            _ports.Remove(id);
        }

        public IOerPort GetPort(int id) => _ports[id];

        public bool CheckConnectionExists(int port1Id, int port2Id)
        {
            if (!_connections.ContainsKey(port1Id)) return false;
            if (!_connections.ContainsKey(port2Id)) return false;
            
            return _connections[port1Id].Datas.Contains(port2Id) &&
                   _connections[port2Id].Datas.Contains(port1Id);
        }

        public void AddConnection(int port1Id, int port2Id)
        {
            if (!_connections.ContainsKey(port1Id)) _connections.Add(port1Id, new());
            _connections[port1Id].Add(port2Id);
            
            if (!_connections.ContainsKey(port2Id)) _connections.Add(port2Id, new());
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