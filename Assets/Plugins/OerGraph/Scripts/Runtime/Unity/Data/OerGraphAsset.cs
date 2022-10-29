using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data.EditorRelated;
using UnityEngine;

namespace OerGraph.Runtime.Unity.Data
{
    /// <summary>
    /// ScriptableObject containing main graph and editor data related to it
    /// </summary>
    public class OerGraphAsset : ScriptableObject
    {
        [SerializeReference] public OerMainGraph Graph;
        public OerGraphEditorData EditorData;
    }
}