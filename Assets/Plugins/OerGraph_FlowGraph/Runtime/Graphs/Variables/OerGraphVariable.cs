using System;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables
{
    [Serializable]
    public abstract class OerGraphVariable<TType>
    {
        [SerializeField] public TType DefaultValue;
        [SerializeField] public TType CurrentValue;

        public OerGraphVariable(TType defaultValue) => DefaultValue = defaultValue;
    }
}