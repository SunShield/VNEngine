using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;
using UnityEngine.UIElements;

namespace OerGraph.Editor.Windows.Elements.SubInspectors
{
    /// <summary>
    /// Sub-inspectors are meant to inspect special parts of a graph,
    /// if default inspector is not enough
    /// </summary>
    public abstract class OerGraphSubInspector : VisualElement
    {
        protected OerGraphAsset Asset { get; private set; }
        protected OerMainGraph Graph { get; private set; }
        
        protected OerGraphSubInspector(OerGraphAsset asset, OerMainGraph graph)
        {
            Asset = asset;
            Graph = graph;
        }
    }
}