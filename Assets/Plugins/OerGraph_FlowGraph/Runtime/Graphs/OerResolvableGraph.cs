using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Service.Classes;
using OerGraph_FlowGraph.Runtime.Service.Classes;
using OerGraph_FlowGraph.Runtime.Tools;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs
{
    [Serializable]
    public class OerResolvableGraph : OerMainGraph
    {
        [field: SerializeReference] public StringToBoolVariableDictionary   BoolVariables { get; private set; }
        [field: SerializeReference] public StringToIntVariableDictionary    IntVariables { get; private set; }
        [field: SerializeReference] public StringToFloatVariableDictionary  FloatVariables { get; private set; }
        [field: SerializeReference] public StringToStringVariableDictionary StringVariables { get; private set; }

        [SerializeReference] [HideInInspector] private StringToFlowNodeDictionary _startingNodes = new();
        public IReadOnlyDictionary<string, OerFlowNode> StartingNodes => _startingNodes;

        public void Initialize()
        {
            BoolVariables = new();
            IntVariables = new();
            FloatVariables = new();
            StringVariables = new();
        }

        protected override void OnPostAddNode(int nodeId)
        {
            var node = Nodes[nodeId];
            if (node is not OerFlowNode ofn) return;
            if (string.IsNullOrEmpty(ofn.StartingNodeKey)) return;

            _startingNodes.Add(ofn.StartingNodeKey, ofn);
        }

        protected override void OnPostRemoveNode(int nodeId)
        {
            var node = Nodes[nodeId];
            if (node is not OerFlowNode ofn) return;
            if (string.IsNullOrEmpty(ofn.StartingNodeKey)) return;

            _startingNodes.Remove(ofn.StartingNodeKey);
        }

        /// <summary>
        /// Runtime-only method
        /// 
        /// Resolves graph
        /// </summary>
        /// <param name="startingNodeName"></param>
        public void Resolve(string startingNodeName)
        {
            var resolver = new OerGraphResolver();
            resolver.ResolveGraph(this, startingNodeName);
        }

        public override OerMainGraph CreateInstance() => new OerResolvableGraph();
    }
}