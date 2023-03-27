using System;
using System.Collections.Generic;
using OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Ports;
using UnityEngine;

namespace OerGraph.Runtime.Core.Graphs.Structure.EditorBased.Elements.Nodes
{
    [Serializable]
    public abstract partial class OerNode
    {
        public int Id;
        public List<int> InPortIds = new();
        public List<int> OutPortIds = new();
        public List<int> InDynamicPortIds = new();
        public List<int> OutDynamicPortIds = new();
        
        /// <summary>
        /// This is used only to show proper title in the Graph
        /// Actually, this is not very properly designed way of approaching this, but it's very simple
        /// Because I wanna avoid creating special mappings for titles
        /// and, also, wanna avoid creating a NodeViews only for changing titles
        /// </summary>
        public abstract string Name { get; }

        private OerNodePortsData _portsData;
        public OerNodePortsData PortsData => _portsData ??= GetPortsData();
        public abstract OerNodePortsData GetPortsData();
        
        /// <summary>
        /// This method is called on node creation through editor window.
        /// It's never called in runtime
        /// </summary>
        /// <param name="id"></param>
        public void Initialize(int id)
        {
            Id = id;
        }

        public void AddPort(int id, OerPortType type)
        {
            if (type == OerPortType.Input) InPortIds.Add(id);
            else                           OutPortIds.Add(id);
        }

        public void RemovePort(int id)
        {
            if (InPortIds.Contains(id))
            {
                InPortIds.Remove(id);
                return;
            }

            OutPortIds.Remove(id);
        }

        public void AddDynamicPort(int id, OerPortType type)
        {
            if (type == OerPortType.Input)
            {
                InDynamicPortIds ??= new();
                InDynamicPortIds.Add(id);
            }
            else
            {
                OutDynamicPortIds ??= new();
                OutDynamicPortIds.Add(id);
            }
        }

        public void RemoveDynamicPort(int id)
        {
            if (InDynamicPortIds.Contains(id))
            {
                InDynamicPortIds.Remove(id);
                if (InDynamicPortIds.Count == 0) InDynamicPortIds = null;
                return;
            }

            OutDynamicPortIds.Remove(id);
            if (OutDynamicPortIds.Count == 0) OutDynamicPortIds = null;
        }
    }
}