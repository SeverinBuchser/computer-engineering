using System;
using System.Text.Json;

namespace BooleanCircuits.Models
{
    [Serializable]
    public class NodeModel : IModel
    {
        public string Name {get; set;} = "";
    
        public void Validate()
        {
            if (Name == "") throw new JsonException("Name of node cannot be empty!");
        }
    }
}
