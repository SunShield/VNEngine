using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Nodes;
using VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Attributes.Checkers
{
    public static class NNodeParamsAttributeChecker
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
    }
}