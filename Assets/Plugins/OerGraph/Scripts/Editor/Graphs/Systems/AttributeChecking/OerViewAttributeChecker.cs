using System;
using System.Collections.Generic;
using OerGrap.Editor.Graphs.Elements.Ports;
using OerGraph.Editor.Graphs.Attributes;
using OerGraph.Editor.Graphs.Elements.Nodes;

namespace OerGraph.Editor.Graphs.Systems.AttributeChecking
{
    public static class OerViewAttributeChecker
    {
        private static readonly Dictionary<Type, OerNodeViewParamsAttribute> _typesCheckedForCustomNodeViewParamsAttr = new();
        private static readonly Dictionary<Type, OerPortViewCustomParamsAttribute> _typesCheckedForCustomNPortViewParamsAttr = new();

        public static OerNodeViewParamsAttribute GetNodeHasParamsAttribute(OerNodeView node)
        {
            var nodeType = node.GetType();

            if (!_typesCheckedForCustomNodeViewParamsAttr.ContainsKey(nodeType))
            {
                _typesCheckedForCustomNodeViewParamsAttr.Add(nodeType, null);
                
                var customAttributes = nodeType.GetCustomAttributes(true);
                foreach (var customAttribute in customAttributes)
                {
                    if (customAttribute is not OerNodeViewParamsAttribute npa) continue;

                    _typesCheckedForCustomNodeViewParamsAttr[nodeType] = npa;
                    break;
                }
            }

            return _typesCheckedForCustomNodeViewParamsAttr[nodeType];
        }
        
        public static OerPortViewCustomParamsAttribute GetPortHasParamsAttribute(OerPortView node)
        {
            var portType = node.GetType();

            if (!_typesCheckedForCustomNPortViewParamsAttr.ContainsKey(portType))
            {
                _typesCheckedForCustomNPortViewParamsAttr.Add(portType, null);
                
                var customAttributes = portType.GetCustomAttributes(true);
                foreach (var customAttribute in customAttributes)
                {
                    if (customAttribute is not OerPortViewCustomParamsAttribute ppa) continue;

                    _typesCheckedForCustomNPortViewParamsAttr[portType] = ppa;
                    break;
                }
            }

            return _typesCheckedForCustomNPortViewParamsAttr[portType];
        }
    }
}