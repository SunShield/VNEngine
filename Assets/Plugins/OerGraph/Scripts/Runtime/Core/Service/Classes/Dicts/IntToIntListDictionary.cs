using System;
using System.Collections.Generic;

namespace OerGraph.Runtime.Core.Service.Classes.Dicts
{
    [Serializable]
    public class IntToIntListDictionary : SerializableDictionary<int, IntList>
    {
    }

    [Serializable]
    public class IntList
    {
        public List<int> Datas = new();
        
        public int this[int index] => Datas[index];
        public int Count => Datas.Count;

        public void Add(int data) => Datas.Add(data);
        public void Remove(int data) => Datas.Remove(data);
        
        public IntList() { }
        public IntList(IntList another) => Datas = new(another.Datas);
    }
}