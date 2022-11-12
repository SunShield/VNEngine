using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements.SubInspectors
{
    /// <summary>
    /// Sub-inspectors are meant to inspect special parts of a graph,
    /// if default inspector is not enough
    /// </summary>
    public abstract class OerGraphSubInspector : VisualElement
    {
        protected OerMainGraph Graph { get; private set; }
        
        protected OerGraphSubInspector(OerMainGraph graph)
        {
            Graph = graph;
        }
    }
}