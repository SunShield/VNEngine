using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Graphs;
using OerGraph_FlowGraph.Editor.GraphAssets;
using OerGraph_FlowGraph.Editor.SubInspectors;
using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Configuration.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Resolvable/GraphMappings", fileName = "Resolvable Graph Mappings")]
    public class OerResolvableGraphMappings : OerGraphMappings
    {
        public override Dictionary<string, Type> GetGraphTypes()
        {
            return new()
            {
                { "Resolvable", typeof(OerResolvableGraph) }
            };
        }

        public override Dictionary<string, Type> GetAssetGraphBuilderTypes()
        {
            return new()
            {
                { "Resolvable", typeof(OerResolvableGraphAssetBuilder) }
            };
        }

        public override Dictionary<Type, Type> GetGraphSubInspectorTypes()
        {
            return new()
            {
                { typeof(OerResolvableGraph), typeof(OerResolvableGraphSubInspector) }
            };
        }
    }
}