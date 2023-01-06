using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.Int
{
    public class OerResolvableGraphSubInspectorIntVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerIntVariableView>
    {
        protected override string Title { get; } = "Int Variables";

        public OerResolvableGraphSubInspectorIntVariablesBlock(OerGraphAsset asset, OerResolvableGraph graph) : base(asset, graph)
        {
            TitleLabel.style.backgroundColor = new Color(1f, 0.6f, 0.4f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var intVariableName in Graph.IntVariables.Keys)
            {
                AddVariableView(intVariableName, Graph.IntVariables[intVariableName].DefaultValue);
            }
        }

        protected override void AddVariableInternal(OerIntVariableView variableView) => Graph.IntVariables.Add(variableView.Name, new OerGraphIntVariable(0));
        protected override void RemoveVariableInternal(string variableName) => Graph.IntVariables.Remove(variableName);
    }
}