using System;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Nodes
{
    public class NNodeParamsAttribute : Attribute
    {
        public string NodeClassName { get; }
        public string NodeStyleSheetPath { get; }
        
        public NNodeParamsAttribute(string nodeClassName = null, string nodeStylesheetPath = null)
        {
            NodeClassName = nodeClassName;
            NodeStyleSheetPath = nodeStylesheetPath;
        }
    }
}