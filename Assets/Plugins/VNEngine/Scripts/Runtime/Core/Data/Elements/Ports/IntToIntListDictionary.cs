using System;
using System.Collections.Generic;

namespace VNEngine.Runtime.Core.Data.Elements.Ports
{
    // TODO: Later search for a serializable hashset, because list here is a big performance issue cause we search this collection for data
    [Serializable]
    public class IntToIntListDictionary : SerializableDictionary<int, IntList> { }

    [Serializable]
    public class IntList
    {
        public List<int> Storage = new();

        public void Add(int i) => Storage.Add(i);
        public void Remove(int i) => Storage.Remove(i);
    }
}