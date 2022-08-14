using System.Collections;
using System.Collections.Generic;
using VNEngine.Runtime.Core.Graphs.Attributes.PortFields;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Systems.Reflection
{
    public static class NNodePortsGetter
    {
        public static List<INPort> GetNodePorts(NNode node)
        {
            var ports = new List<INPort>();
            
            var nodeType = node.GetType();
            var fields = nodeType.GetFields();
            foreach (var fieldInfo in fields)
            {
                var attributes = fieldInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute is NPortAttribute)
                    {
                        var value = fieldInfo.GetValue(node);
                        if (value is INPort port) ports.Add(port);
                        else if (value is IList list)
                        {
                            foreach (var element in list)
                            {
                                var elementTyped = element as INPort;
                                ports.Add(elementTyped);
                            }
                        }
                    }
                }
            }
            
            return ports;
        }

        public static Dictionary<string, (bool isList, List<INPort> ports)> GetNodePortsByFields(NNode node)
        {
            var ports = new Dictionary<string, (bool isList, List<INPort> ports)>();
            
            var nodeType = node.GetType();
            var fields = nodeType.GetFields();
            foreach (var fieldInfo in fields)
            {
                var attributes = fieldInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute is NPortAttribute)
                    {
                        var value = fieldInfo.GetValue(node);
                        if (value is INPort port)
                        {
                            if (!ports.ContainsKey(fieldInfo.Name)) ports.Add(fieldInfo.Name, (false, new()));
                            ports[fieldInfo.Name].ports.Add(port);
                        }
                        else if (value is IList list)
                        {
                            foreach (var element in list)
                            {
                                var elementTyped = element as INPort;
                                if (!ports.ContainsKey(fieldInfo.Name)) ports.Add(fieldInfo.Name, (true, new()));
                                ports[fieldInfo.Name].ports.Add(elementTyped);
                            }
                        }
                    }
                }
            }
            
            return ports;
        }
    }
}