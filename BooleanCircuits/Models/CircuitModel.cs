using System;
using System.Text.Json;

namespace BooleanCircuits.Models
{
    [Serializable]
    public class CircuitModel : IModel
    {
        public string Name {get; set;} = "";
        public string Id {get; set;} = "";
        
        public virtual void Validate()
        {
            if (Name == "") throw new JsonException("Name of circuit cannot be empty!");
            if (Id == "") throw new JsonException("Id of circuit cannot be empty!");
        }
    }
}
