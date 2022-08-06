using System;
using UnityEditor;
using UnityEngine;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Editor.Service.Utilities
{
    public static class NGraphAssetUtility
    {
        public static void CreateGraph(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            var path = EditorUtility.SaveFilePanel("Create new graph asset", "", $"{name}.asset", "");
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            if (string.IsNullOrEmpty(path)) return;

            var so = ScriptableObject.CreateInstance<NGraphAsset>();
            so.RuntimeGraph = new(name);
            AssetDatabase.CreateAsset(so, $"{pathFormatted}");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static NGraphAsset LoadGraph()
        {
            var path = EditorUtility.OpenFilePanel("Choose a graph to open", "", "asset");

            if (string.IsNullOrEmpty(path)) return null;
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            var asset = AssetDatabase.LoadAssetAtPath<NGraphAsset>(pathFormatted);
            return asset;
        }
    }
}