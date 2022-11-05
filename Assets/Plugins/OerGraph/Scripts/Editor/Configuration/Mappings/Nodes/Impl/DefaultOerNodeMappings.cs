﻿using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Nodes.Impl
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
                { "Test/Node1",       typeof(Node1) },
                { "Test/Test2/Node2", typeof(Node2) },
                { "Test/Test2/Node3", typeof(Node3) },
                { "Test/TestOerNode", typeof(TestOerNumberOperationsNode) }
            };
        }
    }
}