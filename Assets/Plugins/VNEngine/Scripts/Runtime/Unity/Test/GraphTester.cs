using UnityEngine;
using VNEngine.Runtime.Unity.Data;
using VNEngine.Runtime.Core.Graphs.Systems.Copying;

namespace VNEngine.Runtime.Unity.Test
{
    public class GraphTester : MonoBehaviour
    {
        [SerializeField] private NGraphAsset _graph;

        public void Start()
        {
            var node = _graph.RuntimeGraph.Nodes[1];
            var copy = NGraphDuplicator.DuplicateGraph(_graph.RuntimeGraph);

            var value = node.GetOutputValue("B", 0);
            Debug.Log(value);
        }
    }
}