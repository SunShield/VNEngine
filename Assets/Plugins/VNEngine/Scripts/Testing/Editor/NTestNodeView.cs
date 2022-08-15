using UnityEngine;
using UnityEngine.UIElements;
using VNEngine.Editor.Graphs.Elements.Nodes;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes;

namespace VNEngine.Testing.Editor
{
    public class NTestNodeView : NNodeView
    {
        public NTestNodeView(NNode node) : base(node)
        {
            style.backgroundColor = new StyleColor(Color.red);
        }
    }
}