using System;
using UnityEngine;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports
{
    [Serializable]
    public abstract partial class OerPort<TType> : IOerPort
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public OerPortType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int NodeId { get; private set; }

        [field: SerializeField] public TType DefaultValue;
        
        /// <summary>
        /// This method is called on port creation through editor window when node is created.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(int id, OerPortType type, string name, int nodeId)
        {
            Id = id;
            Type = type;
            Name = name;
            NodeId = nodeId;
        }

        public Type GetUnderlyingType() => typeof(TType);
    }
}