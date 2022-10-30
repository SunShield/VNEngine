using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes
{
    [Serializable]
    public class OerNodePortsData
    {
        public List<(string portValueType, string portName, OerPortType portType, bool dynamic)> Ports;

        public OerNodePortsData(List<(string, string, OerPortType, bool)> ports) => Ports = ports;
    }
}