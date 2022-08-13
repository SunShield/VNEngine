using UnityEngine.UIElements;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Nodes;
using VNEngine.Runtime.Core.Data.Elements.Ports;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Checkers
{
    public static class NAttributeChecker
    {
        public static NNodeParamsAttribute GetNodeHasParamsAttribute(NNode node)
        {
            var nodeType = node.GetType();
            var customAttributes = nodeType.GetCustomAttributes(true);
            foreach (var customAttribute in customAttributes)
            {
                if (customAttribute is NNodeParamsAttribute npa)
                {
                    return npa;
                }
            }

            return null;
        }
        
        public static NPortParamsAttribute GetPortHasParamsAttribute(INPort node)
        {
            var portType = node.GetType();
            var customAttributes = portType.GetCustomAttributes(true);
            foreach (var customAttribute in customAttributes)
            {
                if (customAttribute is NPortParamsAttribute ppa)
                {
                    return ppa;
                }
            }

            return null;
        }
    }
}