using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes
{
    [Serializable]
    public class OerNodePortsData
    {
        public List<(string portValueType, string portName, OerPortType portType)> Ports;

        public OerNodePortsData(List<(string, string, OerPortType)> ports) => Ports = ports;
    }
}