using System;
using OerGraph.Editor.Service.Prefs;
using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine;

namespace OerGraph.Editor.Service.Utilities
{
    public static class OerGraphAssetUtility
    {
        /*public static void CreateGraph(string name, string graphKey)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return;
                var path = EditorUtility.SaveFilePanel("Create new graph asset", "", $"{name}.asset", "");
                var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
                var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

                if (string.IsNullOrEmpty(path))
                {
                    OerPlayerPrefs.ClearRecentlyCreatedGraphName();
                    return;
                }

                var so = ScriptableObject.CreateInstance<OerGraphAsset>();
                so.Graph = OerGraphCreator.CreateGraph(graphKey);
                AssetDatabase.CreateAsset(so, $"{pathFormatted}");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                OerPlayerPrefs.SetRecentlyCreatedGraphName(pathFormatted);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                OerPlayerPrefs.ClearRecentlyCreatedGraphName();
                throw;
            }
            
        }*/

        public static OerGraphAsset LoadGraph()
        {
            var path = EditorUtility.OpenFilePanel("Choose a graph to open", "", "asset");
            return LoadGraph(path);
        }
        
        public static OerGraphAsset LoadGraph(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            var assetsTextPos = path.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = path.Substring(assetsTextPos, path.Length - assetsTextPos);

            var asset = AssetDatabase.LoadAssetAtPath<OerGraphAsset>(pathFormatted);
            return asset;
        }
    }
}