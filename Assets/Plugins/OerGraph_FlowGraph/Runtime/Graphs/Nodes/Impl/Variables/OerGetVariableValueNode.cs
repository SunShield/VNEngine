using System;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs.Nodes.Impl.Variables
{
    [Serializable]
    public abstract class OerGetVariableValueNode : OerResolvableGraphNode
    {
        [field: SerializeField] [HideInInspector] protected string VariableName { get; private set; }

        public void SetVariableName(string varName) => VariableName = varName;
    }
}