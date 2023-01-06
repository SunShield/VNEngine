﻿using System;
using OerGraph.Editor.GraphAssets.Builders;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using OerGraph.Runtime.Unity.Data;
using OerGraph_FlowGraph.Runtime.Graphs;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using UnityEditor;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.GraphAssets
{
    /// <summary>
    /// Resolvable graph requires:
    /// </summary>
    public class OerResolvableGraphAssetBuilder : OerGraphAssetBuilder
    {
        public override string GetBuildLocation(string graphName)
        {
            var path = EditorUtility.SaveFolderPanel("Create new graph asset", $"{graphName}", "");
            return string.IsNullOrEmpty(path) 
                ? null 
                : path;
        }

        public override OerGraphAsset BuildAsset(string buildLocation, string graphName, OerMainGraph graph)
        {
            var pathFormatted = FormatDirectoryPath(buildLocation);

            CreateGraphDirectory(graphName, pathFormatted);
            var graphAssetPath = ConstructGraphAssetPath(graphName, pathFormatted);
            var so = CreateGraphAsset(graph, graphAssetPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return so;
        }

        private static OerGraphAsset CreateGraphAsset(OerMainGraph graph, string graphAssetPath)
        {
            var so = ScriptableObject.CreateInstance<OerGraphAsset>();
            so.Graph = graph;
            CreateGraphVariables(graphAssetPath, so);
            AssetDatabase.CreateAsset(so, $"{graphAssetPath}");
            return so;
        }

        private static void CreateGraphVariables(string graphAssetPath, OerGraphAsset so)
        {
            var graphTyped = so.Graph as OerResolvableGraph; 
            graphTyped.Initialize();
        }

        private static string FormatDirectoryPath(string buildLocation)
        {
            var assetsTextPos = buildLocation.IndexOf("Assets", StringComparison.InvariantCulture);
            var pathFormatted = buildLocation.Substring(assetsTextPos, buildLocation.Length - assetsTextPos);
            return pathFormatted;
        }

        private static void CreateGraphDirectory(string graphName, string pathFormatted) => AssetDatabase.CreateFolder(pathFormatted, graphName);
        private static string ConstructGraphAssetPath(string graphName, string pathFormatted) => $"{pathFormatted}/{graphName}/{graphName}.asset";
    }
}