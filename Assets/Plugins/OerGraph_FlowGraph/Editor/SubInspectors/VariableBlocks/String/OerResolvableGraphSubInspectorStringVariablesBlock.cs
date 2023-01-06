using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.SubInspectors.VariableBlocks.String
{
    public class OerResolvableGraphSubInspectorStringVariablesBlock : OerResolvableGraphSubInspectorVariablesBlock<OerStringVariableView>
    {
        protected override string Title { get; } = "String Variables";

        public OerResolvableGraphSubInspectorStringVariablesBlock(OerGraphAsset asset, OerResolvableGraph graph) : base(asset, graph)
        {
            TitleLabel.style.backgroundColor = new Color(0.4f, 0.6f, 1f, 0.4f);
        }

        public override void AddExistingVariables()
        {
            foreach (var stringVariableName in Graph.StringVariables.Keys)
            {
                AddVariableView(stringVariableName, Graph.StringVariables[stringVariableName].DefaultValue);
            }
        }

        protected override void AddVariableInternal(OerStringVariableView variableView) => Graph.StringVariables.Add(variableView.Name, new OerGraphStringVariable(""));
        protected override void RemoveVariableInternal(string variableName) => Graph.StringVariables.Remove(variableName);
    }
}