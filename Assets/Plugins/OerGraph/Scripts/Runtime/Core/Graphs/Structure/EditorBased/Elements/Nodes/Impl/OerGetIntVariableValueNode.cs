﻿using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class OerGetIntVariableValueNode : OerGetVariableValueNode
    {
        public override object GetValue(string portName) => MainGraph.IntVariables[VariableName];
        protected override OerNode CreateInstance() => new OerGetIntVariableValueNode();
        public override string Name { get; } = "Get Int Variable";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ( "Int", "Value", OerPortType.Output, false)
            });
        }
    }
}