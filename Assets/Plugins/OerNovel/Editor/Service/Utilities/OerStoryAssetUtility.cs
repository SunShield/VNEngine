using System;
using OerNovel.Runtime.Unity;
using UnityEditor;

namespace OerNovel.Editor.Service.Utilities
{
    public static class OerStoryAssetUtility
    {
        public static OerStoryAsset LoadStory()
        {
            var path = EditorUtility.OpenFilePanel("Choose a graph to open", "", "asset");
            return LoadStory(path);
        }
        
        private static OerStoryAsset LoadStory(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            var asset = AssetDatabase.LoadAssetAtPath<OerStoryAsset>(pathFormatted);
            return asset;
        }
    }
}