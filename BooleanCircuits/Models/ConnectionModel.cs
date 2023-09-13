using System;
using System.Text.Json;

namespace BooleanCircuits.Models
{
    [Serializable]
    public class ConnectionModel : IModel
    {
        public string EmitterCircuit {get; set;} = "";
        public string Emitter {get; set;} = "";
        public string ReceiverCircuit {get; set;} = "";
        public string Receiver {get; set;} = "";
        
        public void Validate()
        {
            if (EmitterCircuit == "") throw new JsonException("Emitter Circuit cannot be empty!");
            if (Emitter == "") throw new JsonException("Emitter cannot be empty!");
            if (ReceiverCircuit == "") throw new JsonException("Receiver Circuit cannot be empty!");
            if (Receiver == "") throw new JsonException("Receiver cannot be empty!");
        }
    }
}
