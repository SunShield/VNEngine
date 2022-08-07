using System;
using UnityEngine;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Runtime.Core.Data.Elements.Ports
{
    public interface INPort
    {
        Type Type { get; }
        string Name { get; }
        NPortType PortType { get; }
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
    }
}