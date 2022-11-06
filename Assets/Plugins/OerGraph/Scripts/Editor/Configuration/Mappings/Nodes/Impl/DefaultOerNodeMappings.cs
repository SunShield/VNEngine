using System;
using System.Collections.Generic;
using OerGraph.Editor.Graphs.Elements.Nodes.Impl;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Nodes.Impl
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/NodeMappings", fileName = "Node Mappings")]
    public class DefaultOerNodeMappings : OerNodeMappings
    {
        public override Dictionary<string, Type> GetRuntimeNodeKeys()
        {
            return new()
            {
                { "Graph/Variables/GetIntVariableValue",    typeof(OerGetIntVariableValueNode)   },
                { "Graph/Variables/GetFloatVariableValue",  typeof(OerGetFloatVariableValueNode) },
                { "Graph/Variables/GetStringVariableValue", typeof(OerGetStringVariableValueNode)},
            };
        }
        
        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {
                { typeof(OerGetIntVariableValueNode),    typeof(OerGetIntVariableValueNodeView)   },
                { typeof(OerGetFloatVariableValueNode),  typeof(OerGetFloatVariableValueNodeView) },
                { typeof(OerGetStringVariableValueNode), typeof(OerGetStringVariableValueNodeView)},
            };
        }
    }
}