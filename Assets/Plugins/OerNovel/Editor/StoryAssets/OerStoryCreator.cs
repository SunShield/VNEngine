using System;
using System.IO;
using OerNovel.Runtime.Stories.Structure;
using OerNovel.Runtime.Unity;
using UnityEditor;
using UnityEngine;

namespace OerNovel.Editor.StoryAssets
{
    public static class OerStoryAssetCreator
    {
        public static string GetCreateLocation(string graphName)
        {
            var path = EditorUtility.SaveFilePanel("Create new story asset", "", $"{graphName}.asset", "");
            if (string.IsNullOrEmpty(path)) return null;
            
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);
            return pathFormatted;
        }
        
        public static OerStoryAsset Create(string buildLocation, string storyName)
        {
            var so = ScriptableObject.CreateInstance<OerStoryAsset>();
            so.Story = CreateStory(storyName);
            AssetDatabase.CreateAsset(so, $"{buildLocation}");

            AssetDatabase.CreateFolder(Path.GetDirectoryName(buildLocation), "Dialogues");
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return so;
        }

        private static OerStory CreateStory(string storyName)
        {
            var story = new OerStory();
            story.Initialize(storyName);
            return story;
        }
    }
}