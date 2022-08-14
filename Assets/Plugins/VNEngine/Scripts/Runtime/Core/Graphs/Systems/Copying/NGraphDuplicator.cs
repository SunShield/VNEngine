using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Runtime.Core.Graphs.Systems.Reflection;
using VNEngine.Runtime.Core.Service.Classes.ListPools;

namespace VNEngine.Runtime.Core.Graphs.Systems.Copying
{
    public static class NGraphDuplicator
    {
        private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> _nodeFieldsByType = new();
        private static readonly Dictionary<string, (bool isList, List<INPort> ports)> _copyingNodePorts = new();
        private static readonly ListPool<INPort> _portListsPool = new();
        
        private static readonly Dictionary<Type, ConstructorInfo> _portContructors = new();
        private static readonly Type[] _portConstructorSignature = { typeof(int) };
        private static readonly object[] _portConstructorData = { 0 };

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
                NNodePortsGetter.GetNodePortsByFields(copyingNode, _copyingNodePorts, _portListsPool);
                
                foreach (var portFieldName in _copyingNodePorts.Keys)
                {
                    var portFieldData = nodeFields[portFieldName];
                    var portsData = _copyingNodePorts[portFieldName];
                    if (!portsData.isList)
                    {
                        var copyingPort = portsData.ports[0];
                        var newPort = ConstructPort(copyingPort.GetType(), copyingPort.Id);
                        newPort.InitializeFromAnother(copyingPort, newNode);
                        newGraph.AddPort(newPort);
                        portFieldData.SetValue(newNode, newPort);
                    }
                    else
                    {
                        var listValue = Activator.CreateInstance(portFieldData.FieldType) as IList;
                        foreach (var copyingPort in portsData.ports)
                        {
                            var newPort = ConstructPort(copyingPort.GetType(), copyingPort.Id);
                            newPort.InitializeFromAnother(copyingPort, newNode);
                            newGraph.AddPort(newPort);
                            listValue.Add(newPort);
                        }
                        portFieldData.SetValue(newNode, listValue);
                    }
                }

                foreach (var value in _copyingNodePorts.Values)
                {
                    _portListsPool.ReturnList(value.ports);
                }
                _copyingNodePorts.Clear();
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

        private static ConstructorInfo GetPortConstructor(Type portType)
        {
            if (!_portContructors.ContainsKey(portType))
            {
                var constructorInfo = portType.GetConstructor(_portConstructorSignature);
                _portContructors.Add(portType, constructorInfo);
            }

            return _portContructors[portType];
        }

        private static INPort ConstructPort(Type portType, int id)
        {
            var constructor = GetPortConstructor(portType);
            _portConstructorData[0] = id;
            var port = constructor.Invoke(_portConstructorData) as INPort;
            return port;
        }
    }
}