using UnityEngine;
using VNEngine.Runtime.Unity.Data;

namespace VNEngine.Plugins.VNEngine.Scripts.Runtime.Unity.Test
{
    public class GraphTester : MonoBehaviour
    {
        [SerializeField] private NGraphAsset _graph;

        public void Start()
        {
            var node = _graph.RuntimeGraph.Nodes[1];

            var value = node.GetOutputValue("B", 0);
            Debug.Log(value);
        }
    }
}