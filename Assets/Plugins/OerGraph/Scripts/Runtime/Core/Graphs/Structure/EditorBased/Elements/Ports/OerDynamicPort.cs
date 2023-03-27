using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    [Serializable]
    public abstract partial class OerDynamicPort<TType> : IOerDynamicPort
    {
        public abstract string PortKey { get; }
        
        public int id;
        public OerPortType type;
        public string name;
        public int nodeId;
        public List<int> portIds;

        public int Id => id;
        public int NodeId => nodeId;
        public OerPortType Type => type;
        public string Name => name;
        public List<int> PortIds => portIds;
        
        /// <summary>
        /// This method is called on port creation through editor window when node is created.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(int id, OerPortType type, string name, int nodeId)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.nodeId = nodeId;
            portIds = new();
        }

        public void RegisterPort(int id) => portIds.Add(id);
        public void UnregisterPort(int id) => portIds.Remove(id);
    }
}