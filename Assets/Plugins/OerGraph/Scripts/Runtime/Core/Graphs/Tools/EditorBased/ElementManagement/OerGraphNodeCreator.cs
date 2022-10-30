using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.ElementManagement
{
    public static class OerGraphNodeCreator
    {
        public static OerNode CreateNode(OerMainGraph mainGraph, string nodeKey, int id)
        {
            // TODO: later can be changed to something more efficient,
            // but this approach is not that bad, after all
            OerNode node = null;
            if (nodeKey == "TestOerNode") 
                node = new TestOerNumberOperationsNode();
            node.Initialize(id);
            CreateNodePorts(mainGraph, node);
            return node;
        }
        
        private static void CreateNodePorts(OerMainGraph mainGraph, OerNode node)
        {
            foreach (var portData in node.PortsData.Ports)
            {
                if (!portData.dynamic)
                {
                    CreateNodePort(mainGraph, node, portData);
                }
                else
                {
                    CreateNodeDynamicPort(mainGraph, node, portData);
                }
            }
        }

        private static void CreateNodeDynamicPort(OerMainGraph mainGraph, OerNode node,
            (string portValueType, string portName, OerPortType portType, bool dynamic) portData)
        {
            var port = mainGraph.AddDynamicPort(portData.portValueType, portData.portName, portData.portType, node.Id);
            node.AddDynamicPort(port.Id, port.Type);
        }

        private static void CreateNodePort(OerMainGraph mainGraph, OerNode node,
            (string portValueType, string portName, OerPortType portType, bool dynamic) portData)
        {
            var port = mainGraph.AddPort(portData.portValueType, portData.portName, portData.portType, node.Id);
            node.AddPort(port.Id, port.Type);
        }
    }
}