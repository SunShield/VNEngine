using System;
using System.Collections.Generic;
using UnityEngine;
using VNEngine.Runtime.Core.Data.Elements.Connections;
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
        [field: SerializeField] public string Name { get; private set; }
        
        /// <summary>
        /// Every created node gets this number as its unique id, then this number is increased by 1
        /// </summary>
        [SerializeField] private int _currentNodeId;
        
        /// <summary>
        /// Every created port gets this number as its unique id, then this number is increased by 1
        /// </summary>
        [SerializeField] private int _currentPortId;

        [SerializeReference] private NodeDictionary _nodes = new();
        [SerializeReference] private PortsDictionary _ports = new();
        [SerializeReference] private NGraphConnections _connections = new();
        
        public IReadOnlyDictionary<int, NNode> Nodes => _nodes;
        public IReadOnlyDictionary<int, INPort> Ports => _ports;
        public IReadOnlyDictionary<int, IntList> Connections => _connections.Storage;
        
        public int NodeId => _currentNodeId++;
        public int PortId => _currentPortId++;

        public NGraph(string name)
        {
            Name = name;
        }

        public void AddNode(NNode node) => _nodes.Add(node.Id, node);
        public void RemoveNode(int id) => _nodes.Remove(id);
        public void AddPort(INPort port) => _ports.Add(port.Id, port);
        public void RemovePort(int id) => _ports.Remove(id);
        public void AddConnection(int port1Id, int port2Id) => _connections.AddConnection(port1Id, port2Id);
        public void RemoveConnection(int port1Id, int port2Id) => _connections.RemoveConnection(port1Id, port2Id);
        public bool CheckConnectionExists(int port1Id, int port2Id) => _connections.CheckConnectionExists(port1Id, port2Id);
    }
}