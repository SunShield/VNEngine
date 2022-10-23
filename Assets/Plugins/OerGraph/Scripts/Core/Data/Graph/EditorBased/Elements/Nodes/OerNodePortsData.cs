using System;
using System.Collections.Generic;
using OerGraph.Core.Data.Graph.EditorBased.Elements.Ports;

namespace OerGraph.Core.Data.Graph.EditorBased.Elements.Nodes
{
    [Serializable]
    public class OerNodePortsData
    {
        public List<(string portValueType, string portName, OerPortType portType)> Ports;

        public OerNodePortsData(List<(string, string, OerPortType)> ports) => Ports = ports;
    }
}