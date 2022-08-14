using UnityEngine;
using VNEngine.Runtime.Core.Graphs.Data;
using VNEngine.Runtime.Unity.Data.EditorRelated;

namespace VNEngine.Runtime.Unity.Data
{
    /// <summary>
    /// Main ScriptableObject
    /// </summary>
    public class NGraphAsset : ScriptableObject
    {
        [SerializeReference] public NGraph RuntimeGraph;
        public NGraphEditorData EditorData;
    }
}