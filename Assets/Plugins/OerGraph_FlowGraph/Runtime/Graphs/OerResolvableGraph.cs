using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes;
using OerGraph.Runtime.Core.Service.Classes.Dicts;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;
using OerGraph_FlowGraph.Runtime.Service.Classes;
using OerGraph_FlowGraph.Runtime.Tools;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs
{
    [Serializable]
    public class OerResolvableGraph : OerMainGraph
    {
        [SerializeReference] [HideInInspector] private StringToFlowNodeDictionary _startingNodes = new();
        public IReadOnlyDictionary<string, OerFlowNode> StartingNodes => _startingNodes;

        [SerializeReference] private StringToIntDictionary _intVariables = new();
        [SerializeReference] private StringToFloatDictionary _floatVariables = new();
        [SerializeReference] private StringToStringDictionary _stringVariables = new();
        public IReadOnlyDictionary<string, int> IntVariables => _intVariables;
        public IReadOnlyDictionary<string, float> FloatVariables => _floatVariables;
        public IReadOnlyDictionary<string, string> StringVariables => _stringVariables;

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
        
        public void SetIntVariable(string variableName, int value) => _intVariables[variableName] = value;
        public void SetFloatVariable(string variableName, float value) => _floatVariables[variableName] = value;
        public void SetStringVariable(string variableName, string value) => _stringVariables[variableName] = value;
    }
}