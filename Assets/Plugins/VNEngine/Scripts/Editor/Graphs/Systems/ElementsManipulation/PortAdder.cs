using System.Reflection;
using VNEngine.Editor.Graphs.Elements.Ports;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class PortAdder
    {
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
            var portView = new NPortView(graphView, port, name, type, port.Type);
            var nodeView = graphView.Nodes[node.Id];
            nodeView.AddPort(portView);
        }
    }
}