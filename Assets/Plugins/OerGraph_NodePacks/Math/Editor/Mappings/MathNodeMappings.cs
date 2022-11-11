using System;
using System.Collections.Generic;
using OerGraph.Editor.Configuration.Mappings.Nodes;
using OerGraph_NodePacks.Math.Editor.Graphs.Nodes;
using OerGraph_NodePacks.Math.Runtime.Nodes.Arithmetics;
using OerGraph_NodePacks.Math.Runtime.Nodes.Comparsion;
using OerGraph_NodePacks.Math.Runtime.Nodes.Functions;
using UnityEngine;

namespace OerGraph_NodePacks.Math.Editor.Mappings
{
    [CreateAssetMenu(menuName = "OerGraph/Config/Mappings/Math/NodeMappings", fileName = "Math Node Mappings")]
    public class MathNodeMappings : OerNodeMappings
    {
        public override (Dictionary<string, Type> nodeKeys, HashSet<Type> graphTypes) GetRuntimeNodeKeys()
        {
            return (new()
            {
                { "Math/Arithmetics/Division", typeof(DivisionNode) },
                { "Math/Arithmetics/Module",   typeof(ModulationNode) },
                { "Math/Arithmetics/Multiply", typeof(MultiplyNode) },
                { "Math/Arithmetics/Subtract", typeof(SubtractNode) },
                { "Math/Arithmetics/Sum",      typeof(SumNode) },
                
                { "Math/Comparison/Max",       typeof(MaxNode) },
                { "Math/Comparison/Min",       typeof(MinNode) },
                { "Math/Comparison/=",         typeof(EqualsNode) },
                { "Math/Comparison/>",         typeof(GreaterThenNode) },
                { "Math/Comparison/>=",        typeof(GreaterThenOrEqualsNode) },
                { "Math/Comparison/<",         typeof(LessThenNode) },
                { "Math/Comparison/<=",        typeof(LessThenOrEqualsNode) },
                
                { "Math/Functions/Average",    typeof(AverageNode) },
                { "Math/Functions/Power",      typeof(PowerNode) },
                { "Math/Functions/Root",       typeof(RootNode) },
            }, 
                null);
        }

        public override Dictionary<Type, Type> GetRuntimeNodeToViewMappings()
        {
            return new()
            {
                { typeof(DivisionNode),            typeof(MathNodeView) },
                { typeof(ModulationNode),          typeof(MathNodeView) },
                { typeof(MultiplyNode),            typeof(MathNodeView) },
                { typeof(SubtractNode),            typeof(MathNodeView) },
                { typeof(SumNode),                 typeof(MathNodeView) },
                                                                                  
                { typeof(MaxNode),                 typeof(MathNodeView) },
                { typeof(MinNode),                 typeof(MathNodeView) },
                { typeof(EqualsNode),              typeof(MathNodeView) },
                { typeof(GreaterThenNode),         typeof(MathNodeView) },
                { typeof(GreaterThenOrEqualsNode), typeof(MathNodeView) },
                { typeof(LessThenNode),            typeof(MathNodeView) },
                { typeof(LessThenOrEqualsNode),    typeof(MathNodeView) },
                
                { typeof(AverageNode),             typeof(MathNodeView) },
                { typeof(PowerNode),               typeof(MathNodeView) },
                { typeof(RootNode),                typeof(MathNodeView) },
            };
        }
    }
}