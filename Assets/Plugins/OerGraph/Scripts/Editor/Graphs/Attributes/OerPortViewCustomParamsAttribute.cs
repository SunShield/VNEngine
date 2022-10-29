using System;

namespace OerGraph.Editor.Graphs.Attributes
{
    public class OerPortViewCustomParamsAttribute : Attribute
    {
        public string ClassName { get; }
        public string StyleSheetPath { get; }
        
        public OerPortViewCustomParamsAttribute(string className = null, string stylesheetPath = null)
        {
            ClassName = className;
            StyleSheetPath = stylesheetPath;
        }
    }
}