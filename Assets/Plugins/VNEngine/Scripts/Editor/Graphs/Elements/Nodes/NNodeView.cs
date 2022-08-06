using UnityEditor.Experimental.GraphView;
using VNEngine.Plugins.VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes;

namespace VNEngine.Scripts.Editor.Graphs.Elements.Nodes
{
    public class NNodeView : Node
    {
        private NNode _node;
        
        public int Id => _node.Id;
        
        public NNodeView(NNode node)
        {
            _node = node;
        }
    }
}
