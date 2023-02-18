using System;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data.EditorRelated;
using UnityEngine;

namespace OerGraph.Runtime.Unity.Data
{
    [Serializable]
    public class OerGraphData
    {
        public string Name;
        public string Key;
        [SerializeReference] public OerMainGraph Graph;
        public OerGraphEditorData EditorData;
    }
}