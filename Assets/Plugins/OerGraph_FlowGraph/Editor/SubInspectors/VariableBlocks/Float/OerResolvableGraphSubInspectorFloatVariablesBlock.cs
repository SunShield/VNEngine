using OerGraph_FlowGraph.Runtime.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Float
{
    public class OerResolvableGraphSubInspectorFloatVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerFloatVariableView>
    {
        protected override string Title { get; } = "Float Variables";

        public OerResolvableGraphSubInspectorFloatVariablesBlock(OerResolvableGraph graph) : base(graph)
        {
            TitleLabel.style.backgroundColor = new Color(1f, 0.2f, 0.2f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var floatVariableName in Graph.Variables.FloatVariables.Keys)
            {
                AddVariableView(floatVariableName, Graph.Variables.FloatVariables[floatVariableName]);
            }
        }

        protected override void AddVariableInternal(OerFloatVariableView variableView) => Graph.Variables.AddFloatVariable(variableView.Name, 0f);
        protected override void RemoveVariableInternal(string variableName) => Graph.Variables.RemoveFloatVariable(variableName);
    }
}