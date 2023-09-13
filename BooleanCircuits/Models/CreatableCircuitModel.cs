using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BooleanCircuits.Models
{
    [Serializable]
    public class CreatableCircuitModel : IModel
    {
        public string Name {get; set;} = "";
        public List<NodeModel> Inputs {get; set;} = new List<NodeModel>();
        public List<NodeModel> Outputs {get; set;} = new List<NodeModel>();
        public List<CircuitModel> Circuits {get; set;} = new List<CircuitModel>();
        public List<ConnectionModel> Connections {get; set;} = new List<ConnectionModel>();
        
        public void Validate()
        {
            if (Name == "") throw new JsonException("Name of circuit cannot be empty!");
            if (Inputs.Count == 0) throw new JsonException("Circuit has to contain at least one input!");
            Inputs.ForEach(input => input.Validate());
            if (Outputs.Count == 0) throw new JsonException("Circuit has to contain at least one output!");
            Outputs.ForEach(output => output.Validate());
            if (Connections.Count == 0) throw new JsonException("Circuit has to contain at least one connection!");
            Connections.ForEach(connection => connection.Validate());
            Circuits.ForEach(circuit => circuit.Validate());
        }

        public CircuitModel ToCircuitModel(string id)
        {
            return new CircuitModel
            {
                Name = Name,
                Id = id
            };
        }
    }
}
