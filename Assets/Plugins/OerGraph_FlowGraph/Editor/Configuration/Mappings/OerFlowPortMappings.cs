using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Ports;
using OerGraph_FlowGraph.Editor.Graph.Elements.Ports;
using OerGraph_FlowGraph.Runtime.Graphs.Ports;
using Plugins.OerGraph_FlowGraph.Runtime.Graphs.Ports;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Configuration.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Resolvable/PortMappings", fileName = "Flow Port Mappings")]
    public class OerFlowPortMappings : OerPortMappings
    {
        public override Dictionary<string, Type> GetRuntimePortKeys()
        {
            return new()
            {
                { "Flow",  typeof(OerFlowPort) },
            };
        }

        public override Dictionary<string, Type> GetRuntimePortDynamicKeys()
        {
            return new()
            {
                { "Flow",  typeof(OerFlowDynamicPort) },
            };
        }

        public override Dictionary<Type, Type> GetRuntimePortToViewMappings()
        {
            return new()
            {
                { typeof(OerFlowPort),  typeof(OerFlowPortView) },
            };
        }

        public override Dictionary<Type, Type> GetRuntimeDynamicPortToViewMappings()
        {
            return new()
            {
                { typeof(OerFlowPort),  typeof(OerFlowDynamicPortView) },
            };
        }
    }
}