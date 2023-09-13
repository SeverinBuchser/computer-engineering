using System.Collections.Generic;
using BooleanCircuits.Helper.Emitter;

namespace BooleanCircuits.Nodes
{
    public class GatewayNode : Node, IHashableEmitter, IHashableReceiver
    {    
        private readonly List<IReceiver> _receivers = new List<IReceiver>();

        public GatewayNode(string name) : base(name) {}
        public GatewayNode(string name, bool value) : base(name, value) {}

        public virtual void AddReceiver(IReceiver receiver)
        {
            if (!_receivers.Contains(receiver))
            {
                _receivers.Add(receiver);
                receiver.Receive(Value);
            }
        }
        
        public virtual void RemoveReceiver(IReceiver receiver) => _receivers.Remove(receiver);
        protected override void OnValueChange() => _receivers.ForEach(receiver => receiver.Receive(Value));
        
        public void Receive(bool value) => Value = value;
    }
}
