using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Helper.Serializable;
using BooleanCircuits.Models;
using BooleanCircuits.Nodes;

namespace BooleanCircuits
{
    public abstract class Circuit : IHashable, ISerializable
    {        
        protected readonly HashMap<GatewayNode> Inputs = new HashMap<GatewayNode>();
        protected readonly HashMap<GatewayNode> Outputs = new HashMap<GatewayNode>();
        
        public readonly string Name;
        public readonly string Id;
        protected Circuit(string name, string id) => (Name, Id) = (name, id);

        public virtual GatewayNode GetInput(string name) => Inputs.Get(name);
        public virtual GatewayNode GetOutput(string name) => Outputs.Get(name);
        
        public virtual bool ContainsInput(string name) => Inputs.Contains(name);
        public virtual bool ContainsOutput(string name) => Outputs.Contains(name);
    
        public string Hash() => Id;
        public override string ToString() => Name + ":" + Id;
    
        public virtual IModel Serialize()
        {
           return new CircuitModel()
           {
               Name = Name,
               Id = Id
           };
        }
    }
}
