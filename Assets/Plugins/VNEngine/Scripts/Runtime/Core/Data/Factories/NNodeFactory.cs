﻿using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes.Implementations;

namespace VNEngine.Runtime.Core.Data.Factories
{
    public class NNodeFactory
    {
        public NNode ConstructNode(NGraph graph, string nodeType)
        {
            var node = ContructByType(graph, nodeType, graph.NodeId);
            graph.AddNode(node);
            return node;
        }

        private NNode ContructByType(NGraph graph, string nodeType, int id) => nodeType switch
        {
            "BasicNode" => new NBasicTestNode(graph, id)
        };
    }
}