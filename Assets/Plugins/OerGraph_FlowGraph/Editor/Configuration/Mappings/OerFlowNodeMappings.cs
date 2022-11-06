using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_FlowGraph.Editor.Graph.Elements.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables;
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
                { "FlowControl/If",                    typeof(OerIfFlowNode)                 },
                { "Graph/Variables/SetIntVariable",    typeof(OerSetIntVariableValueNode)    },
                { "Graph/Variables/SetFloatVariable",  typeof(OerSetFloatVariableValueNode)  },
                { "Graph/Variables/SetStringVariable", typeof(OerSetStringVariableValueNode) }
            };
        }

        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {
                { typeof(OerSetIntVariableValueNode),    typeof(OerSetIntVariableValueNodeView) },
                { typeof(OerSetFloatVariableValueNode),  typeof(OerSetFloatVariableValueNodeView) },
                { typeof(OerSetStringVariableValueNode), typeof(OerSetStringVariableValueNodeView) }
            };
        }
    }
}