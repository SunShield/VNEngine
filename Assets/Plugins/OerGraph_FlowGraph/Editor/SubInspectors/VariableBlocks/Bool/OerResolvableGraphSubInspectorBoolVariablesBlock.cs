using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Bool
{
    public class OerResolvableGraphSubInspectorBoolVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerBoolVariableView>
    {
        protected override string Title { get; } = "Bool Variables";

        public OerResolvableGraphSubInspectorBoolVariablesBlock(OerGraphAsset asset, OerResolvableGraph graph) : base(asset, graph)
        {
            TitleLabel.style.backgroundColor = new Color(0.6f, 0.6f, 0.2f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var boolVariableName in Graph.BoolVariables.Keys)
            {
                AddVariableView(boolVariableName, Graph.BoolVariables[boolVariableName].DefaultValue);
            }
        }

        protected override void AddVariableInternal(OerBoolVariableView variableView) => Graph.BoolVariables.Add(variableView.Name, new OerGraphBoolVariable(false));
        protected override void RemoveVariableInternal(string variableName) => Graph.BoolVariables.Remove(variableName);
        
    }
}