using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using UnityEngine;

namespace OerGraph.Editor.Configuration.Mappings.Graphs.Impl
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/GraphMappings", fileName = "Graph Mappings")]
    public class DefaultOerGraphMappings : OerGraphMappings
    {
        public override Dictionary<string, Type> GetGraphTypes()
        {
            return new()
            {
                { "Default", typeof(OerMainGraph) }
            };
        }
    }
}