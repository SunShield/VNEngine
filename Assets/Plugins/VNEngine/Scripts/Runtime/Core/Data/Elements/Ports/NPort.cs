using System;
using UnityEngine;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Service.Extensions;

namespace VNEngine.Runtime.Core.Data.Elements.Ports
{
    public interface INPort
    {
        Type Type { get; }
        int Id { get; }
        NPortType PortType { get; }
        NNode Node { get; }
        void Initialize(NNode ownerNode, NPortType portType);
        bool IsCompatibleWith(INPort other);
    }
    
    /// <summary>
    /// Class for runtime port
    /// </summary>
    [Serializable]
    public class NPort<TType> : INPort
    {
        [SerializeReference] private TType _backingValue;
        
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeReference] public NNode Node { get; private set; }
        [field: SerializeReference] public NPortType PortType { get; private set; }

        public Type Type => typeof(TType);

        public NPort(int id) => Id = id;

        public void Initialize(NNode ownerNode, NPortType portType)
        {
            Node = ownerNode;
            PortType = portType;
            _backingValue = default;
        }
        
        public bool IsCompatibleWith(INPort other)
        {
            if (other.Node == Node || other.PortType == PortType) return false;
            
            return (other.PortType == NPortType.Input && Type.IsCastableTo(other.Type, true)) ||
                   (other.PortType == NPortType.Output && other.Type.IsCastableTo(Type, true));
        }
    }
}