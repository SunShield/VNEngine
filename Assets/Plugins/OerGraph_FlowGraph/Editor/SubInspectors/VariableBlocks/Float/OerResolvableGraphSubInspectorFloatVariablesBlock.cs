using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Float
{
    public class OerResolvableGraphSubInspectorFloatVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerFloatVariableView>
    {
        protected override string Title { get; } = "Float Variables";

        public OerResolvableGraphSubInspectorFloatVariablesBlock(OerGraphAsset asset, OerResolvableGraph graph) : base(asset, graph)
        {
            TitleLabel.style.backgroundColor = new Color(1f, 0.2f, 0.2f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var floatVariableName in Graph.FloatVariables.Keys)
            {
                AddVariableView(floatVariableName, Graph.FloatVariables[floatVariableName].DefaultValue);
            }
        }

        protected override void AddVariableInternal(OerFloatVariableView variableView) => Graph.FloatVariables.Add(variableView.Name, new OerGraphFloatVariable(0f));
        protected override void RemoveVariableInternal(string variableName) => Graph.FloatVariables.Remove(variableName);
    }
}