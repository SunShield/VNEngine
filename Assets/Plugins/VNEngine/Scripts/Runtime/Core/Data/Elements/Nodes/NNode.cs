using System;
using UnityEngine;

namespace VNEngine.Scripts.Runtime.Core.Data.Elements.Nodes
{
    /// <summary>
    /// Runtime node class
    /// </summary>
    [Serializable]
    public class NNode
    {
        [field: SerializeField] public int Id { get; private set; }

        public NNode(int id)
        {
            Id = id;
        }
    }
}