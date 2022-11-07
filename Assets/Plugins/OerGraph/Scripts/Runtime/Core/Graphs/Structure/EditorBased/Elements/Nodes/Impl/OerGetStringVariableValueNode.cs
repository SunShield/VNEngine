﻿using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes.Impl
{
    public class OerGetStringVariableValueNode : OerGetVariableValueNode
    {
        public override object GetValue(string portName) => MainGraph.StringVariables[VariableName];
        protected override OerNode CreateInstance() => new OerGetStringVariableValueNode();
        public override string Name { get; } = "Get String Variable";
        
        public override OerNodePortsData GetPortsData()
        {
            return new(new()
            {
                ( "String", "Value", OerPortType.Output, false)
            });
        }
    }
}