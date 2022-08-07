using System.Reflection;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Editor.Graphs.Factories;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Editor.Graphs.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class PortManager
    {
        private static NPortViewConstructor _portViewConstructor = new();
        
        public static void AddAllNodePorts(NNode node, NGraphView graphView)
        {
            var nodeType = node.GetType();
            var fieldsData = nodeType.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            foreach (var fieldData in fieldsData)
            {
                var attributes = fieldData.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is InputAttribute)
                    {
                        var port = fieldData.GetValue(node) as INPort;
                        AddPort(port, node, fieldData.Name, NPortType.Input, graphView);
                    }

                    if (attribute is OutputAttribute)
                    {
                        var port = fieldData.GetValue(node) as INPort;
                        AddPort(port, node, fieldData.Name, NPortType.Output, graphView);
                    }
                }
            }
        }

        public static void AddPort(INPort port, NNode node, string name, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, name, type);
            var nodeView = graphView.Nodes[node.Id];
            _portViewConstructor.Construct(port, nodeView, name, type, graphView);
        }

        public static void AddAllNodeExistingPorts(NNode runtimeNode, NNodeView nodeView, NGraphView graphView)
        {
            var nodeType = runtimeNode.GetType();
            var fieldsData = nodeType.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            foreach (var fieldData in fieldsData)
            {
                var attributes = fieldData.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is InputAttribute or OutputAttribute)
                    {
                        var port = fieldData.GetValue(runtimeNode) as INPort;
                        AddExistingPort(nodeView, port, port.Name, port.PortType, graphView);
                    }
                }
            }
        }

        public static void AddExistingPort(NNodeView nodeView, INPort port, string name, NPortType type, NGraphView graphView)
        {
            _portViewConstructor.Construct(port, nodeView, name, type, graphView);
        }
    }
}