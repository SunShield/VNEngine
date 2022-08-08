using System.Reflection;
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
                        AddPort(port, fieldData.Name, node, NPortType.Input, graphView);
                    }

                    if (attribute is OutputAttribute)
                    {
                        var port = fieldData.GetValue(node) as INPort;
                        
                        AddPort(port, fieldData.Name, node, NPortType.Output, graphView);
                    }
                }
            }
        }

        public static void AddPort(INPort port, string fieldName, NNode node, NPortType type, NGraphView graphView)
        {
            port.Initialize(node, type);
            var nodeView = graphView.Nodes[node.Id];
            _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);
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
                        AddExistingPort(nodeView, fieldData.Name, port, port.PortType, graphView);
                    }
                }
            }
        }

        public static void AddExistingPort(NNodeView nodeView, string fieldName, INPort port, NPortType type, NGraphView graphView)
        {
            _portViewConstructor.Construct(port, fieldName, nodeView, type, graphView);
        }
    }
}