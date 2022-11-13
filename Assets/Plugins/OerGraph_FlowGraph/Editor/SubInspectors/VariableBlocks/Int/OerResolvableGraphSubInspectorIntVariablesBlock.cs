using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Int
{
    public class OerResolvableGraphSubInspectorIntVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerIntVariableView>
    {
        protected override string Title { get; } = "Int Variables";

        public OerResolvableGraphSubInspectorIntVariablesBlock(OerResolvableGraph graph) : base(graph)
        {
            TitleLabel.style.backgroundColor = new Color(1f, 0.6f, 0.4f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var intVariableName in Graph.Variables.IntVariables.Keys)
            {
                AddVariableView(intVariableName, Graph.Variables.IntVariables[intVariableName]);
            }
        }

        protected override void AddVariableInternal(OerIntVariableView variableView) => Graph.Variables.AddIntVariable(variableView.Name, 0);
        protected override void RemoveVariableInternal(string variableName) => Graph.Variables.RemoveIntVariable(variableName);
    }
}