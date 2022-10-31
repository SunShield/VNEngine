using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Impl
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/NodeMappings", fileName = "Node Mappings")]
    public class DefaultOerNodeMappings : OerNodeMappings
    {
        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {

            };
        }

        public override Dictionary<string, Type> GetRuntimeNodeKeys()
        {
            return new()
            {
                { "TestOerNode", typeof(TestOerNumberOperationsNode) }
            };
        }
    }
}