using System;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine;

namespace OerGraph.Editor.GraphAssets.Builders.Impl
{
    public class DefaultGraphAssetBuilder : OerGraphAssetBuilder
    {
        public override string GetBuildLocation(string assetName)
        {
            var path = EditorUtility.SaveFilePanel("Create new graph asset", "", $"{assetName}.asset", "");
            if (string.IsNullOrEmpty(path)) return null;
            
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);
            return pathFormatted;
        }
        
        public override OerGraphAsset BuildAsset(string buildLocation, string graphName)
        {
            var so = ScriptableObject.CreateInstance<OerGraphAsset>();
            so.Graphs = new();
            AssetDatabase.CreateAsset(so, $"{buildLocation}");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return so;
        }
    }
}