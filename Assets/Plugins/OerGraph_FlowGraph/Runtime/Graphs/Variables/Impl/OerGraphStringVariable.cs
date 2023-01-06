using System;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl
{
    [Serializable]
    public class OerGraphStringVariable : OerGraphVariable<string>
    {
        public OerGraphStringVariable(string defaultValue) : base(defaultValue)
        {
        }
    }
}