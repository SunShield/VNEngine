using System;

namespace OerGraph.Editor.Graphs.Attributes
{
    public class OerNodeViewParamsAttribute : Attribute
    {
        public string ClassName { get; }
        public string StyleSheetPath { get; }
        
        public OerNodeViewParamsAttribute(string className = null, string stylesheetPath = null)
        {
            ClassName = className;
            StyleSheetPath = stylesheetPath;
        }
    }
}