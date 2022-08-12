using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VNEngine.Runtime.Core.Data;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Runtime.Core.Attributes.Ports;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes
{
    /// <summary>
    /// Runtime node class
    /// </summary>
    [Serializable]
    public class NNode
    {
        [field: SerializeReference] public NGraph Graph { get; private set; }
        [field: SerializeReference] public int Id { get; private set; }

        public NNode(NGraph graph, int id)
        {
            Graph = graph;
            Id = id;
        }

        public List<INPort> GetPorts()
        {
            var ports = new List<INPort>();
            
            var nodeType = GetType();
            var fields = nodeType.GetFields();
            foreach (var fieldInfo in fields)
            {
                var attributes = fieldInfo.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is NInputAttribute or NOutputAttribute)
                    {
                        var value = fieldInfo.GetValue(this);
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
    }
}