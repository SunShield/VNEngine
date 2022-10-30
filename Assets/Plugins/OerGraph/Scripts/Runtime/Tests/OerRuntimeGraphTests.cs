using OerGraph.Runtime.Core.Graphs.Structure.EditorBased;
using UnityEngine;

namespace OerGraph.Runtime.Tests
{
    public class OerRuntimeGraphTests : MonoBehaviour
    {
        private void Start()
        {
            var graph = new OerMainGraph();
            graph.AddNode("TestOerNode");
            graph.AddPortToDynamicPort("Int", "Number 1", 0, 0);
            graph.AddPortToDynamicPort("Int", "Number 2", 0, 0);
            graph.AddPortToDynamicPort("Int", "Number 3", 0, 0);
            graph.AddPortToDynamicPort("Int", "Number 4", 0, 0);

            var graphCopy = graph.Copy();
            var t = "test";
        }
    }
}