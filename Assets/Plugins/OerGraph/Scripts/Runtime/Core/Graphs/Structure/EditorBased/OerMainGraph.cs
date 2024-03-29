﻿using System;
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
        [SerializeField] [HideInInspector] private int _currentDynPortId;
        
        [SerializeField] [HideInInspector] private IntToOerNodeDictionary _nodes = new();
        [SerializeField] [HideInInspector] private IntToOerPortDictionary _ports = new();
        [SerializeField] [HideInInspector] private IntToOerDynamicPortDictionary _dynPorts = new();
        [SerializeField] [HideInInspector] private IntToIntListDictionary _connections = new();

        public IReadOnlyDictionary<int, OerNode> Nodes => _nodes;
        public IDictionary<int, IntList> Connections => _connections;

        public OerNode AddNode(string key)
        {
            var node = OerNodeFactory.CreateNode(this, key, _currentNodeId++);
            _nodes.Add(node.Id, node);
            OnPostAddNode(node.Id);
            return node;
        }
        
        protected virtual void OnPostAddNode(int nodeId) { }

        public void RemoveNode(int id)
        {
            var node = _nodes[id];
            
            foreach (var portId in node.InPortIds)
                RemovePort(portId);
            
            foreach (var portId in node.OutPortIds)
                RemovePort(portId);

            if (node.InDynamicPortIds != null)
            {
                foreach (var inDynamicPortId in node.InDynamicPortIds)
                    RemoveDynamicPort(inDynamicPortId);
            }

            if (node.OutDynamicPortIds != null)
            {
                foreach (var outDynamicPortId in node.OutDynamicPortIds)
                    RemoveDynamicPort(outDynamicPortId);
            }
            
            OnPostRemoveNode(node.Id);
            _nodes.Remove(id);
        }
        
        protected virtual void OnPostRemoveNode(int nodeId) { }

        public OerNode GetNode(int id) => _nodes[id];

        public IOerPort AddPort(string key, string name, OerPortType portType, int nodeId)
        {
            var port = OerPortFactory.CreatePort(key, _currentPortId++, name, portType, nodeId);
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

        public IOerDynamicPort AddDynamicPort(string key, string name, OerPortType portType, int nodeId)
        {
            var port = OerPortFactory.CreateDynamicPort(key, _currentDynPortId++, name, portType, nodeId);
            _dynPorts.Add(port.Id, port);
            return port;
        }
        
        public IOerPort AddPortToDynamicPort(string key, string name, int nodeId, int dynPortId)
        {
            var dynPort = _dynPorts[dynPortId];
            var port = AddPort(key, name, dynPort.Type, nodeId);
            dynPort.RegisterPort(port.Id);
            return port;
        }

        public void RemovePortFromDynamicPort(int id, int dynPortId)
        {
            _dynPorts[dynPortId].UnregisterPort(id);
            RemovePort(id);
        }

        public void RemoveDynamicPort(int id)
        {
            var dynamicPort = _dynPorts[id];
            var portIds = new List<int>(dynamicPort.PortIds);
            foreach (var portId in portIds)
            {
                RemovePort(portId);
            }

            _dynPorts.Remove(id);
        }

        public IOerDynamicPort GetDynamicPort(int id) => _dynPorts[id];

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