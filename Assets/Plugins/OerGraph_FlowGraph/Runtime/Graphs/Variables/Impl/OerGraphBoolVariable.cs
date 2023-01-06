using System;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl
{
    [Serializable]
    public class OerGraphBoolVariable : OerGraphVariable<bool>
    {
        public OerGraphBoolVariable(bool defaultValue) : base(defaultValue)
        {
        }
    }
}