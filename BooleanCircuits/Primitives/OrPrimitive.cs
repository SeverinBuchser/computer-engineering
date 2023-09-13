using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Nodes;

namespace BooleanCircuits.Primitives
{
    public sealed class OrPrimitive : Circuit
    {    
        public OrPrimitive(string id) : base("OR", id)
        {
            GatewayNode input1 = new GatewayNode("Input 1");
            GatewayNode input2 = new GatewayNode("Input 2");
            GatewayNode output = new GatewayNode("Output");

            Inputs.Add(input1);
            Inputs.Add(input2);
            Outputs.Add(output);
            
            
            input1.AddReceiver(new CallbackReceiver(_ => output.Receive(input1 | input2)));
            input2.AddReceiver(new CallbackReceiver(_ => output.Receive(input1 | input2)));
        }
    }
}