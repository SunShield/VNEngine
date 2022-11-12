using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_FlowGraph.Editor.Graph.Elements.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Control;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Configuration.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Resolvable/NodeMappings", fileName = "Flow Node Mappings")]
    public class OerFlowNodeMappings : OerNodeMappings
    {
        public override (Dictionary<string, Type> nodeKeys, HashSet<Type> graphTypes) GetRuntimeNodeKeys()
        {
            return (new()
            {
                { "FlowControl/Start",                      typeof(OerDefaultStartNode)           },
                { "FlowControl/If",                         typeof(OerIfFlowNode)                 },
                { "FlowControl/Switch",                     typeof(OerSwitchNode)                 },
                
                { "Graph/Variables/GetBoolVariableValue",   typeof(OerGetBoolVariableValueNode)   },
                { "Graph/Variables/GetIntVariableValue",    typeof(OerGetIntVariableValueNode)    },
                { "Graph/Variables/GetFloatVariableValue",  typeof(OerGetFloatVariableValueNode)  },
                { "Graph/Variables/GetStringVariableValue", typeof(OerGetStringVariableValueNode) },
                { "Graph/Variables/SetBoolVariable",        typeof(OerSetBoolVariableValueNode)   },
                { "Graph/Variables/SetIntVariable",         typeof(OerSetIntVariableValueNode)    },
                { "Graph/Variables/SetFloatVariable",       typeof(OerSetFloatVariableValueNode)  },
                { "Graph/Variables/SetStringVariable",      typeof(OerSetStringVariableValueNode) }
            }, 
            new ()
            {
                typeof(OerResolvableGraph)
            });
        }

        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {
                { typeof(OerDefaultStartNode),           typeof(OerFlowStartNodeView)              },
                { typeof(OerIfFlowNode),                 typeof(OerFlowControlNodeView)            },
                { typeof(OerSwitchNode),                 typeof(OerFlowControlNodeView)            },
                                   
                { typeof(OerGetBoolVariableValueNode),   typeof(OerGetBoolVariableValueNodeView)   },
                { typeof(OerGetIntVariableValueNode),    typeof(OerGetIntVariableValueNodeView)    },
                { typeof(OerGetFloatVariableValueNode),  typeof(OerGetFloatVariableValueNodeView)  },
                { typeof(OerGetStringVariableValueNode), typeof(OerGetStringVariableValueNodeView) },
                { typeof(OerSetBoolVariableValueNode),   typeof(OerSetBoolVariableValueNodeView)   },
                { typeof(OerSetIntVariableValueNode),    typeof(OerSetIntVariableValueNodeView)    },
                { typeof(OerSetFloatVariableValueNode),  typeof(OerSetFloatVariableValueNodeView)  },
                { typeof(OerSetStringVariableValueNode), typeof(OerSetStringVariableValueNodeView) }
            };
        }
    }
}