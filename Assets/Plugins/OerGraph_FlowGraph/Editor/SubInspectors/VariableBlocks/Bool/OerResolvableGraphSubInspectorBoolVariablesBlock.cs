using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Bool
{
    public class OerResolvableGraphSubInspectorBoolVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerBoolVariableView>
    {
        protected override string Title { get; } = "Bool Variables";

        public OerResolvableGraphSubInspectorBoolVariablesBlock(OerResolvableGraph graph) : base(graph)
        {
            TitleLabel.style.backgroundColor = new Color(0.6f, 0.6f, 0.2f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var boolVariableName in Graph.Variables.BoolVariables.Keys)
            {
                AddVariableView(boolVariableName, Graph.Variables.BoolVariables[boolVariableName]);
            }
        }

        protected override void AddVariableInternal(OerBoolVariableView variableView) => Graph.Variables.AddBoolVariable(variableView.Name, false);
        protected override void RemoveVariableInternal(string variableName) => Graph.Variables.RemoveBoolVariable(variableName);
    }
}