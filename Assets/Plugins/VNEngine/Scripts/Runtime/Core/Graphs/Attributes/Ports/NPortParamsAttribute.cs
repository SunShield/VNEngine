using System;

namespace VNEngine.Runtime.Core.Graphs.Attributes.Ports
{
    public class NPortParamsAttribute : Attribute
    {
        public string PortClassName { get; }
        public string PortStyleSheetPath { get; }
        
        public NPortParamsAttribute(string portClassName = null, string portStylesheetPath = null)
        {
            PortClassName = portClassName;
            PortStyleSheetPath = portStylesheetPath;
        }
    }
}