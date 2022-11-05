using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics;
using OerGraph_NodePacks.Math.Runtime.Nodes.Comparsion;
using OerGraph_NodePacks.Math.Runtime.Nodes.Functions;
using UnityEngine;

namespace OerGraph_NodePacks.Math.Editor.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Math/NodeMappings", fileName = "Math Node Mappings")]
    public class MathNodeMappings : OerNodeMappings
    {
        public override Dictionary<string, Type> GetRuntimeNodeKeys()
        {
            return new()
            {
                { "Math/Arithmetics/Division", typeof(DivisionNode) },
                { "Math/Arithmetics/Module", typeof(ModulationNode) },
                { "Math/Arithmetics/Multiply", typeof(MultiplyNode) },
                { "Math/Arithmetics/Subtract", typeof(SubtractNode) },
                { "Math/Arithmetics/Sum", typeof(SumNode) },
                
                { "Math/Comparsion/Max", typeof(MaxNode) },
                { "Math/Comparsion/Min", typeof(MinNode) },
                
                { "Math/Functions/Average", typeof(AverageNode) },
                { "Math/Functions/Power", typeof(PowerNode) },
                { "Math/Functions/Root", typeof(RootNode) },
            };
        }
    }
}