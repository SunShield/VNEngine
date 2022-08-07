using System;
using UnityEngine;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Service.Extensions;

namespace VNEngine.Runtime.Core.Data.Elements.Ports
{
    public interface INPort
    {
        Type Type { get; }
        string Name { get; }
        NPortType PortType { get; }
        NNode Node { get; }
        bool IsCompatibleWith(INPort other);
        void Initialize(NNode ownerNode, string name, NPortType portType);
    }
    
    /// <summary>
    /// Class for runtime port
    /// </summary>
    [Serializable]
    public class NPort<TType> : INPort
    {
        [SerializeField] private TType _backingValue;
        
        [field: SerializeField]     public string Name { get; private set; }
        [field: SerializeReference] public NNode Node { get; private set; }
        [field: SerializeField]     public NPortType PortType { get; private set; }

        public Type Type => typeof(TType);

        public void Initialize(NNode ownerNode, string name, NPortType portType)
        {
            Node = ownerNode;
            Name = name;
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