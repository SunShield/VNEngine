using OerGraph.Runtime.Core.Service.Classes.Dicts;
using UnityEngine;

namespace OerGraph.Runtime.Unity.Data
{
    /// <summary>
    /// ScriptableObject containing main graph and editor data related to it
    /// </summary>
    public class OerGraphAsset : ScriptableObject
    {
        [SerializeReference] public StringToOerGraphDataDictionary Graphs;
    }
}