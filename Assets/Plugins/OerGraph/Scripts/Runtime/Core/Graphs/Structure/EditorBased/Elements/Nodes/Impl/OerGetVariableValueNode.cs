using System;
using UnityEngine;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    [Serializable]
    public abstract class OerGetVariableValueNode : OerNode
    {
        [field: SerializeField] [HideInInspector] protected string VariableName { get; private set; }

        public void SetVariableName(string varName) => VariableName = varName;
    }
}