using System;
using System.Collections.Generic;
using UnityEngine;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Runtime.Core.Data
{
    /// <summary>
    /// Main graph class.
    /// It's intended to be used in runtime, copied, modified etc
    /// </summary>
    [Serializable]
    public class NGraph
    {
        /// <summary>
        /// Every created node gets this number as its unique id, then this number is increased by 1
        /// </summary>
        [SerializeField] private int _currentNodeId;
        
        /// <summary>
        /// Every created port gets this number as its unique id, then this number is increased by 1
        /// </summary>
        [SerializeField] private int _currentPortId;

        [SerializeReference] private NodeDictionary _nodes = new();
        
        // To avoid recursive serializations and other bullshit,
        // we just serialize port connections as is matrix instead of some kind of complicated arrays inside ports, containing ports etc
        [SerializeReference] private IntToIntListDictionary _connections = new();
        
        public IReadOnlyDictionary<int, NNode> Nodes => _nodes;
        public IReadOnlyDictionary<int, IntList> Connections => _connections;

        [field: SerializeField] public string Name { get; private set; }
        
        public int NodeId => _currentNodeId++;
        public int PortId => _currentPortId++;

        public NGraph(string name)
        {
            Name = name;
        }

        public void AddNode(NNode node)
        {
            _nodes.Add(node.Id, node);
        }
        
        public void RemoveNode(int id)
        {
            _nodes.Remove(id);
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
            _connections[port2Id].Remove(port1Id);

            if (_connections[port1Id].Storage.Count == 0) _connections.Remove(port1Id);
            if (_connections[port2Id].Storage.Count == 0) _connections.Remove(port2Id);
        }
    }
}