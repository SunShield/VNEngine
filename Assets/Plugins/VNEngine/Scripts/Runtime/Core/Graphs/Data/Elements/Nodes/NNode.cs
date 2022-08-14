using System;
using UnityEngine;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Nodes
{
    /// <summary>
    /// Runtime node class
    /// </summary>
    [Serializable]
    public abstract class NNode
    {
        [field: SerializeReference] public NGraph Graph { get; private set; }
        [field: SerializeReference] public int Id { get; private set; }

        protected NNode(NGraph graph, int id)
        {
            Graph = graph;
            Id = id;
        }

        public abstract object GetOutputValue(string portName, int index = 0);

        public NNode Copy(NGraph newGraph)
        {
            var newNode = CreateInstance(Id, newGraph);
            ProcessNodePostCopy(newNode, newGraph);
            return newNode;
        }

        public abstract NNode CreateInstance(int id, NGraph newGraph);
        public virtual void ProcessNodePostCopy(NNode newNode, NGraph newGraph) { }
    }
}