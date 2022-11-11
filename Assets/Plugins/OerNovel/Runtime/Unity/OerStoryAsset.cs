using OerNovel.Runtime.Stories.Structure;
using UnityEngine;

namespace OerNovel.Runtime.Unity
{
    public class OerStoryAsset : ScriptableObject
    {
        [SerializeReference] private OerStory _story;
    }
}