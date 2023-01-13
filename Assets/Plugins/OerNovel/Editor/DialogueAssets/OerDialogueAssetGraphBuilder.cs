using System.IO;
using OerGraph.Runtime.Unity.Data;
using OerNovel.Runtime.Graphs.Tools;
using OerNovel.Runtime.Unity;
using UnityEditor;
using UnityEngine;

namespace OerNovel.Editor.DialogueAssets
{
    // TODO: refactor and merge with OerGraphAssetBuilder in somewhat way (?)
    public static class OerDialogueAssetGraphBuilder
    {
        public static OerGraphAsset Build(OerStoryAsset asset, string graphName)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            var storyDirectory = Path.GetDirectoryName(path);
            var dialoguesPath = $"{storyDirectory}/Dialogues";

            var dialogue = OerDialogueGraphCreator.Create();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var dialogueAssetPath = $"{dialoguesPath}/{graphName}.asset";
            var so = ScriptableObject.CreateInstance<OerGraphAsset>();
            so.Graph = dialogue;
            dialogue.Initialize();
            AssetDatabase.CreateAsset(so, $"{dialogueAssetPath}");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return so;
        }
    }
}