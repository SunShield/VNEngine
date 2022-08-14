using System;
using System.Collections.Generic;
using UnityEngine;
using VNEngine.Runtime.Core.Graphs.Data.Elements.Ports;

namespace VNEngine.Runtime.Core.Graphs.Data.Elements.Connections
{
    [Serializable]
    public class NGraphConnections
    {
        // To avoid recursive serializations and other bullshit,
        // we just serialize port connections as is matrix instead of some kind of complicated arrays inside ports, containing ports etc
        [SerializeReference] private IntToIntListDictionary _storage = new();
        public IReadOnlyDictionary<int, IntList> Storage => _storage;
        
        public void AddConnection(int port1Id, int port2Id)
        {
            if (!_storage.ContainsKey(port1Id)) _storage.Add(port1Id, new());
            _storage[port1Id].Add(port2Id);
            
            if (!_storage.ContainsKey(port2Id)) _storage.Add(port2Id, new());
            _storage[port2Id].Add(port1Id);
        }

        public void RemoveConnection(int port1Id, int port2Id)
        {
            if (!_storage.ContainsKey(port1Id) ||
                !_storage.ContainsKey(port2Id)) return;
            
            if(_storage[port1Id].Storage.Contains(port2Id)) _storage[port1Id].Remove(port2Id);
            if(_storage[port2Id].Storage.Contains(port1Id)) _storage[port2Id].Remove(port1Id);

            if (_storage[port1Id].Storage.Count == 0) _storage.Remove(port1Id);
            if (_storage[port2Id].Storage.Count == 0) _storage.Remove(port2Id);
        }

        public bool CheckConnectionExists(int port1Id, int port2Id) => _storage.ContainsKey(port1Id) && _storage[port1Id].Contains(port2Id);
    }
}