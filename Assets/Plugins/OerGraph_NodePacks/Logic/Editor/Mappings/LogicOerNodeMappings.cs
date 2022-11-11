using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_NodePacks.Logic.Editor.Nodes;
using OerGraph_NodePacks.Logic.Runtime.Nodes;
using UnityEngine;

namespace OerGraph_NodePacks.Logic.Editor.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Logic/NodeMappings", fileName = "Logic Node Mappings")]
    public class LogicOerNodeMappings : OerNodeMappings
    {
        public override (Dictionary<string, Type> nodeKeys, HashSet<Type> graphTypes) GetRuntimeNodeKeys()
        {
            return (new()
            {
                { "Logic/&&", typeof(AndNode) },
                { "Logic/||", typeof(OrNode)  },
                { "Logic/^",  typeof(XorNode) },
                { "Logic/!",  typeof(NotNode) },
            }, 
                null);
        }

        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {
                { typeof(AndNode), typeof(LogicNodeView) },
                { typeof(OrNode),  typeof(LogicNodeView) },
                { typeof(XorNode), typeof(LogicNodeView) },
                { typeof(NotNode), typeof(LogicNodeView) },
            };
        }
    }
}