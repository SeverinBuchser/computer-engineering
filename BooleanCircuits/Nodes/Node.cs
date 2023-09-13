using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Helper.Serializable;
using BooleanCircuits.Models;

namespace BooleanCircuits.Nodes
{    
    public abstract class Node : ISerializable, IHashable
    {
        public string Name {get;}
        private bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnValueChange();
                }
            }
        }
        
        protected Node(string name) : this(name, false) {}
        protected Node(string name, bool value) => (Name, Value) = (name, value);
    
        protected abstract void OnValueChange();
    
        public virtual string Hash() => Name;
        public IModel Serialize() => new NodeModel { Name = Name };
    
        public static bool operator& (Node firstNode, Node secondNode) => firstNode.Value && secondNode.Value;    
        public static bool operator| (Node firstNode, Node secondNode) => firstNode.Value || secondNode.Value;    
        public static bool operator! (Node node) => !node.Value;
        
    }
}


