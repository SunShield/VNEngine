using System;
using BlueGraph.Editor;
using UnityEngine;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Ports
{
    public interface INPort
    {
        Type Type { get; }
        int Id { get; }
        string StaticName { get; }
        int DynIndex { get; }
        NPortType PortType { get; }
        bool HasBackingField { get; }
        
        NNode Node { get; }
        void Initialize(NNode ownerNode, string name, NPortType portType);
        bool IsCompatibleWith(INPort other);
        void SetIndex(int index);
        object GetValue();
    }
    
    /// <summary>
    /// Class for runtime port
    /// </summary>
    [Serializable]
    public abstract class NPort<TType> : INPort
    {
        [SerializeReference] protected TType BackingValue;
        
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string StaticName { get; private set; }
        [field: SerializeField] public int DynIndex { get; private set; }
        [field: SerializeField] public bool HasBackingField { get; set; } = true;
        [field: SerializeReference] public NNode Node { get; private set; }
        [field: SerializeReference] public NPortType PortType { get; private set; }

        protected NGraph Graph => Node.Graph;

        public Type Type => typeof(TType);

        protected NPort(int id) => Id = id;

        public void Initialize(NNode ownerNode, string name, NPortType portType)
        {
            Node = ownerNode;
            StaticName = name;
            PortType = portType;
            BackingValue = default;
        }

        public void SetIndex(int index) => DynIndex = index;
        
        public bool IsCompatibleWith(INPort other)
        {
            if (other.Node == Node || other.PortType == PortType) return false;
            
            return (other.PortType == NPortType.Input && Type.IsCastableTo(other.Type, true)) ||
                   (other.PortType == NPortType.Output && other.Type.IsCastableTo(Type, true));
        }

        public object GetValue()
        {
            if (PortType == NPortType.Input)
            {
                if (!Graph.Connections.ContainsKey(Id)) return BackingValue;
                var connectedPortIds = Graph.Connections[Id];

                // we support port multi-connection but NOT getting a value from multiple connections;
                var firstPortId = connectedPortIds.Storage[0];
                var firstPort = Graph.Ports[firstPortId];
                return firstPort.GetValue();
            }
            else
            {
                return Node.GetOutputValue(StaticName, DynIndex);
            }
        }

        /// <summary>
        /// Used in graph copying process
        /// </summary>
        /// <param name="another"></param>
        /// <param name="newNode"></param>
        /// <param name="backingValue"></param>
        public void InitializeFromAnother(INPort another, NNode newNode, object backingValue)
        {
            Id = another.Id;
            StaticName = another.StaticName;
            DynIndex = another.DynIndex;
            HasBackingField = another.HasBackingField;
            Node = newNode;
            BackingValue = (TType)backingValue;
        }
    }
}