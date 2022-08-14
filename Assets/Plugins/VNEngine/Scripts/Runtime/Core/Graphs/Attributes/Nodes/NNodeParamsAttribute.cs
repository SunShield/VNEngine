using System;

namespace VNEngine.Runtime.Core.Graphs.Attributes.Nodes
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