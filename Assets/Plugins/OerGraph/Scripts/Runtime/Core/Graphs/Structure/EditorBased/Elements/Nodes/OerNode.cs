using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEngine;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes
{
    [Serializable]
    public abstract partial class OerNode
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public List<int> InPortIds { get; private set; } = new();
        [field: SerializeField] public List<int> OutPortIds { get; private set; } = new();

        public abstract OerNodePortsData GetPortsData();

        /// <summary>
        /// This method is called on node creation through editor window.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(OerMainGraph mainGraph, int id)
        {
            Id = id;
            var portsData = GetPortsData();
            CreatePorts(mainGraph, portsData);
        }
        
        /// <summary>
        /// Creates ports from PortData when node is created from editor graph
        /// </summary>
        /// <param name="mainGraph"></param>
        public void CreatePorts(OerMainGraph mainGraph, OerNodePortsData portsData)
        {
            foreach (var portData in portsData.Ports)
            {
                var port = mainGraph.AddPort(portData.portValueType, portData.portName, portData.portType, Id);
                AddPort(port.Id, port.Type);
            }
        }

        public void AddPort(int id, OerPortType type)
        {
            if (type == OerPortType.Input) InPortIds.Add(id);
            else                           OutPortIds.Add(id);
        }

        public void RemovePort(int id)
        {
            if (InPortIds.Contains(id))
            {
                InPortIds.Remove(id);
                return;
            }

            OutPortIds.Remove(id);
        }
    }
}