﻿using System.Collections.Generic;
using System.Linq;
using OerGraph.Editor.Graphs;
using UnityEngine;

namespace OerGraph_FlowGraph.Editor.Graph.Elements.Nodes
{
    public class OerSetStringVariableValueNodeView: OerSetVariableValueNodeView
    {
        public OerSetStringVariableValueNodeView(OerGraphView view, int runtimeNodeId) : base(view, runtimeNodeId)
        {
            titleContainer.style.backgroundColor = new Color(0.4f, 0.6f, 1f, 0.4f);
        }

        protected override List<string> GetVariableNames() => View.Graph.StringVariables.Keys.ToList();
    }
}