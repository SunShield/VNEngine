using System.Reflection;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Ports;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Data.Elements.Ports;

namespace VNEngine.Editor.Graphs.Systems.ElementsManipulation
{
    public static class PortAdder
    {
        public static void AddAllNodePorts(NNode node)
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
                        AddPort(port, node, fieldData.Name, NPortType.Input);
                    }

                    if (attribute is OutputAttribute)
                    {
                        var port = fieldData.GetValue(node) as INPort;
                        AddPort(port, node, fieldData.Name, NPortType.Input);
                    }
                }
            }
        }

        public static void AddPort(INPort port, NNode node, string name, NPortType type)
        {
            port.Initialize(node, name, type);
        }
    }
}