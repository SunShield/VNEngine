using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Configuration.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Resolvable/NodeMappings", fileName = "Flow Node Mappings")]
    public class OerFlowNodeMappings : OerNodeMappings
    {
        public override Dictionary<string, Type> GetRuntimeNodeKeys()
        {
            return new()
            {
                { "FlowControl/If", typeof(OerIfFlowNode) },
            };
        }
    }
}