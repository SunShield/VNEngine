using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;
using VNEngine.Runtime.Core.Service.Classes.ListPools;

namespace VNEngine.Runtime.Core.Graphs.Systems.Reflection
{
    public static class NNodePortsGetter
    {
        // caching reflected data for performance
        private static readonly Dictionary<Type, List<(bool isList, FieldInfo field)>> _portFieldsByNodeTypes = new();

        public static List<INPort> GetNodePorts(NNode node)
        {
            var ports = new List<INPort>();
            
            var portFieldsDatas = GetNodeTypePortFields(node.GetType());
            foreach (var portFieldData in portFieldsDatas)
            {
                var value = portFieldData.field.GetValue(node);
                if (!portFieldData.isList)
                {
                    ports.Add(value as INPort);
                }
                else
                {
                    var dynamicPorts = value as IList;
                    foreach (var dynamicPort in dynamicPorts)
                    {
                        ports.Add(dynamicPort as INPort);
                    }
                }
            }
            
            return ports;
        }

        public static void GetNodePortsByFields(NNode node, Dictionary<string, (bool isList, List<INPort> ports)> nodePortsByFields, ListPool<INPort> portListsPool)
        {
            var portFieldsData = GetNodeTypePortFields(node.GetType());
            foreach (var portFieldData in portFieldsData)
            {
                var value = portFieldData.field.GetValue(node);
                var fieldName = portFieldData.field.Name;
                if (!portFieldData.isList)
                {
                    if (!nodePortsByFields.ContainsKey(fieldName)) nodePortsByFields.Add(fieldName, (false, portListsPool.GetList()));
                    nodePortsByFields[fieldName].ports.Add(value as INPort);
                }
                else
                {
                    var dynamicPorts = value as IList;
                    foreach (var dynamicPort in dynamicPorts)
                    {
                        if (!nodePortsByFields.ContainsKey(fieldName)) nodePortsByFields.Add(fieldName, (true, portListsPool.GetList()));
                        nodePortsByFields[fieldName].ports.Add(dynamicPort as INPort);
                    }
                }
            }
        }

        private static List<(bool isList, FieldInfo field)> GetNodeTypePortFields(Type nodeType)
        {
            if (!_portFieldsByNodeTypes.ContainsKey(nodeType))
            {
                var nodeTypePortFields = new List<(bool isList, FieldInfo field)>();
            
                var fields = nodeType.GetFields();
                foreach (var fieldInfo in fields)
                {
                    if (typeof(INPort).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        nodeTypePortFields.Add((false, fieldInfo));
                    }
                    else
                    {
                        // Checking if field is ports list.
                        // these specific checks are used to directly check fieldInfo arguments without using .GetCustomAttributes()
                        // this is done because GetCustomAttributes() generates ABSURD amount of garbage due to how attributes work
                        // (attribute instances are created EVERY TIME attributes are fetched from types of fields)
                        if (!typeof(IList).IsAssignableFrom(fieldInfo.FieldType)) continue;
                        if (!typeof(INPort).IsAssignableFrom(fieldInfo.FieldType.GetGenericArguments()[0])) continue;

                        nodeTypePortFields.Add((true, fieldInfo));
                    }
                }
                
                _portFieldsByNodeTypes.Add(nodeType, nodeTypePortFields);
            }

            return _portFieldsByNodeTypes[nodeType];
        }
    }
}