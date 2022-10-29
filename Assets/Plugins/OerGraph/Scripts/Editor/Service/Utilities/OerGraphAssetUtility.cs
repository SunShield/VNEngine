using System;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine;

namespace OerGraph.Editor.Service.Utilities
{
    public static class OerGraphAssetUtility
    {
        public static void CreateGraph(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            var path = EditorUtility.SaveFilePanel("Create new graph asset", "", $"{name}.asset", "");
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            if (string.IsNullOrEmpty(path)) return;

            var so = ScriptableObject.CreateInstance<OerGraphAsset>();
            so.Graph = new();
            AssetDatabase.CreateAsset(so, $"{pathFormatted}");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static OerGraphAsset LoadGraph()
        {
            var path = EditorUtility.OpenFilePanel("Choose a graph to open", "", "asset");

            if (string.IsNullOrEmpty(path)) return null;
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            var asset = AssetDatabase.LoadAssetAtPath<OerGraphAsset>(pathFormatted);
            return asset;
        }
    }
}