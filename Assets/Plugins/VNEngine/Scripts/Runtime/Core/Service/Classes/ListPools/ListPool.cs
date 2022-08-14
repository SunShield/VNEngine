using System;
using System.Collections.Generic;

namespace VNEngine.Runtime.Core.Service.Classes.ListPools
{
    public class ListPool<TType>
    {
        private HashSet<List<TType>> _usedLists = new();
        private List<List<TType>> _freeLists = new();

        public List<TType> GetList()
        {
            if (_freeLists.Count == 0) _freeLists.Add(new ());
            
            var list = _freeLists[^1];
            _freeLists.RemoveAt(_freeLists.Count - 1);
            _usedLists.Add(list);
            return list;
        }

        public void ReturnList(List<TType> list)
        {
            if (!_usedLists.Contains(list)) throw new ArgumentException("List does not belong to this pool");

            list.Clear();
            _usedLists.Remove(list);
            _freeLists.Add(list);
        }
    }
}