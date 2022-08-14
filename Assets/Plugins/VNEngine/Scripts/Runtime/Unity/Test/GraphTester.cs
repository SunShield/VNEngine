using UnityEngine;
using VNEngine.Runtime.Core.Graphs.Systems.Copying;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Runtime.Unity.Test
{
    public class GraphTester : MonoBehaviour
    {
        [SerializeField] private NGraphAsset _graph;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var node = _graph.RuntimeGraph.Nodes[1];
                for (int i = 0; i < 100; i++)
                {
                    var copy = NGraphDuplicator.DuplicateGraph(_graph.RuntimeGraph);
                }
    
                var value = node.GetOutputValue("B", 0);
            }
        }
    }
}