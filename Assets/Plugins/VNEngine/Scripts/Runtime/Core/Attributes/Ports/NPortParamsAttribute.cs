using System;

namespace UnityEngine.UIElements
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