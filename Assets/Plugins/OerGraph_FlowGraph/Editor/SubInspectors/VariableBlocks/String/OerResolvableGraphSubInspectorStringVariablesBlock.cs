using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.String
{
    public class OerResolvableGraphSubInspectorStringVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerStringVariableView>
    {
        protected override string Title { get; } = "String Variables";

        public OerResolvableGraphSubInspectorStringVariablesBlock(OerResolvableGraph graph) : base(graph)
        {
            TitleLabel.style.backgroundColor = new Color(0.4f, 0.6f, 1f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var stringVariableName in Graph.Variables.StringVariables.Keys)
            {
                AddVariableView(stringVariableName, Graph.Variables.StringVariables[stringVariableName]);
            }
        }

        protected override void AddVariableInternal(OerStringVariableView variableView) => Graph.Variables.AddStringVariable(variableView.Name, "");
        protected override void RemoveVariableInternal(string variableName) => Graph.Variables.RemoveStringVariable(variableName);
    }
}