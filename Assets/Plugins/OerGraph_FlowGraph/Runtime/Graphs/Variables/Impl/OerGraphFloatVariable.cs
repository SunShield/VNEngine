using System;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl
{
    [Serializable]
    public class OerGraphFloatVariable : OerGraphVariable<float>
    {
        public OerGraphFloatVariable(float defaultValue) : base(defaultValue)
        {
        }
    }
}