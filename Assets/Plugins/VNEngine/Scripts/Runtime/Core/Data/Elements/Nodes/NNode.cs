using System;
using UnityEngine;
using VNEngine.Runtime.Core.Data;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes
{
    /// <summary>
    /// Runtime node class
    /// </summary>
    [Serializable]
    public class NNode
    {
        [field: SerializeReference] public int Id { get; private set; }

        public NNode(NGraph graph, int id)
        {
            Id = id;
        }
    }
}