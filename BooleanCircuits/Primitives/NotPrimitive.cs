using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Nodes;

namespace BooleanCircuits.Primitives
{
    public sealed class NotPrimitive : Circuit
    {    
        public NotPrimitive(string id) : base("NOT", id)
        {
            GatewayNode input = new GatewayNode("Input");
            GatewayNode output = new GatewayNode("Output");
            Inputs.Add(input);
            Outputs.Add(output);
            
            input.AddReceiver(new CallbackReceiver(value => output.Receive(!value)));
            output.Receive(!input);
        }
    }
}