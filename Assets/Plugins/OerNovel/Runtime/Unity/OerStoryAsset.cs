using OerNovel.Runtime.Service.Classes;
using OerNovel.Runtime.Stories.Structure;
using UnityEngine;

namespace OerNovel.Runtime.Unity
{
    public class OerStoryAsset : ScriptableObject
    {
        [SerializeReference] public OerStory Story;
        [SerializeField] public StringToOerGraphAssetDictionary DialogueAssets;
    }
}