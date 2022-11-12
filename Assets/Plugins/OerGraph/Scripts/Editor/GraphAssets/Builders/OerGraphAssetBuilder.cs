using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;

namespace OerGraph.Editor.GraphAssets.Builders
{
    /// <summary>
    /// Uase this class to control the flow of building GraphAssets
    /// and add some additional steps to it if needed
    /// </summary>
    public abstract class OerGraphAssetBuilder
    {
        public abstract string GetBuildLocation(string graphName);
        public abstract OerGraphAsset BuildAsset(string buildLocation, string graphName, OerMainGraph graph);
    }
}