using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Runtime.Core.Graphs.Systems.Reflection;

namespace VNEngine.Runtime.Core.Graphs.Systems.Copying
{
    public static class NGraphDuplicator
    {
        private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> _nodeFieldsByType = new();

        public static NGraph DuplicateGraph(NGraph graphToCopy)
        {
            var newGraph = new NGraph(graphToCopy.Name);

            foreach (var nodeId in graphToCopy.Nodes.Keys)
            {
                var copyingNode = graphToCopy.Nodes[nodeId];
                var newNode = copyingNode.Copy(newGraph);
                newGraph.AddNode(newNode);
            }

            foreach (var nodeId in graphToCopy.Nodes.Keys)
            {
                var copyingNode = graphToCopy.Nodes[nodeId];
                var newNode = newGraph.Nodes[nodeId];

                var nodeFields = GetOrAddBakedNodeFieldsData(copyingNode.GetType());
                var copyingNodePorts = NNodePortsGetter.GetNodePortsByFields(copyingNode);
                foreach (var portFieldName in copyingNodePorts.Keys)
                {
                    var portFieldData = nodeFields[portFieldName];
                    var portsData = copyingNodePorts[portFieldName];
                    if (!portsData.isList)
                    {
                        var copyingPort = portsData.ports[0];
                        var newPort = Activator.CreateInstance(copyingPort.GetType(), copyingPort.Id);
                        portFieldData.SetValue(newNode, newPort);
                        newGraph.AddPort(newPort as INPort);
                    }
                    else
                    {
                        var listValue = Activator.CreateInstance(portFieldData.FieldType);
                        foreach (var copyingPort in portsData.ports)
                        {
                            var newPort = Activator.CreateInstance(copyingPort.GetType(), copyingPort.Id);
                            (listValue as IList).Add(newPort);
                            newGraph.AddPort(newPort as INPort);
                        }
                        portFieldData.SetValue(newNode, listValue);
                    }
                }
            }

            foreach (var portId in graphToCopy.Connections.Keys)
            {
                foreach (var connectedPortId in graphToCopy.Connections[portId].Storage)
                {
                    newGraph.AddConnection(portId, connectedPortId);
                }
            }

            return newGraph;
        }

        private static Dictionary<string, FieldInfo> GetOrAddBakedNodeFieldsData(Type nodeType)
        {
            if (_nodeFieldsByType.TryGetValue(nodeType, out var fieldsData)) return fieldsData;
            
            var nodeFields = nodeType.GetFields().ToDictionary(x => x.Name);
            _nodeFieldsByType.Add(nodeType, nodeFields);

            return nodeFields;
        }
    }
}