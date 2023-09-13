using System;
using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Nodes;

namespace BooleanCircuits
{
    public class CreatableCircuit : Circuit
    {
        public CreatableCircuit(string name, string id,
            Action<HashMap<GatewayNode>, HashMap<GatewayNode>> create) :
            base(name, id) => create(Inputs, Outputs);
    }
}

