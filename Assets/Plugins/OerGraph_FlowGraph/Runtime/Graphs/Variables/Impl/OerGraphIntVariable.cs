using System;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl
{
    [Serializable]
    public class OerGraphIntVariable : OerGraphVariable<int>
    {
        public OerGraphIntVariable(int defaultValue) : base(defaultValue)
        {
        }
    }
}