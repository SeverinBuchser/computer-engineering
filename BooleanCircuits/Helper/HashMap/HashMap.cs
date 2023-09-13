using System;
using System.Collections;
using System.Collections.Generic;

namespace BooleanCircuits.Helper.HashMap
{
    public class HashMap<T>: IEnumerable<T> where T: IHashable {
        protected readonly Dictionary<string, T> Dictionary = new Dictionary<string, T>();
    
        public void Add(T element) => Dictionary.Add(element.Hash(), element);
    
        public void Remove(T element) => Remove(element.Hash());
        public virtual void Remove(string hash) => Dictionary.Remove(hash);
        
        public T Get(string hash)
        {
            if (!Contains(hash)) throw new KeyNotFoundException();
            return Dictionary[hash];
        }
        
        public bool Contains(T element) => Contains(element.Hash());
        public bool Contains(string hash) => Dictionary.ContainsKey(hash);
        
        public void ForEach(Action<T> callback)
        {
            foreach(T hashable in this) callback(hashable);
        }
        
        public List<TReturn> Map<TReturn>(Func<T, TReturn> callback)
        {
            List<TReturn> list = new List<TReturn>();
            foreach(T hashable in this) list.Add(callback(hashable));
            return list;
        }
        
        public List<string> GetHashList() => new List<string>(Dictionary.Keys);
        public IEnumerator<T> GetEnumerator() => Dictionary.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
