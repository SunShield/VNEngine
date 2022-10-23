using System;
using System.Collections.Generic;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports;
using UnityEngine;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes
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
        public void Initialize(OerGraph graph, int id)
        {
            Id = id;
            var portsData = GetPortsData();
            CreatePorts(graph, portsData);
        }
        
        /// <summary>
        /// Creates ports from PortData when node is created from editor graph
        /// </summary>
        /// <param name="graph"></param>
        public void CreatePorts(OerGraph graph, OerNodePortsData portsData)
        {
            foreach (var portData in portsData.Ports)
            {
                var port = graph.AddPort(portData.portValueType, portData.portName, portData.portType, Id);
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