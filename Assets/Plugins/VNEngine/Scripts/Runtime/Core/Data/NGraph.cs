using System;
using System.Collections.Generic;
using UnityEngine;
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
        public IReadOnlyDictionary<int, NNode> Nodes => _nodes;

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
    }
}