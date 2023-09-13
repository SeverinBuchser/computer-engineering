using System;
using System.Collections.Generic;

namespace BooleanCircuits.Helper.HashMap
{
    public class ObservableHashMap<T> : HashMap<T> where T : IHashable 
    {
        private readonly List<Action<T>> _observers = new List<Action<T>>();
        
        public IDisposable Subscribe(Action<T> observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return new Subscription(_observers, observer);
        }
        
        public override void Remove(string hash)
        {
            if (Contains(hash)) _observers.ForEach(observer => observer(Get(hash)));
            base.Remove(hash);
        }
        
        private class Subscription : IDisposable
        {
            private readonly ICollection<Action<T>> _observers;
            private readonly Action<T> _observer;
            public Subscription(ICollection<Action<T>> observers, Action<T> observer) =>
                (_observers, _observer) = (observers, observer);
            
            public void Dispose() => _observers.Remove(_observer);
        }
    }
}
