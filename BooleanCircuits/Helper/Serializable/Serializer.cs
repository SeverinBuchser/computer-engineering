using System.Text.Encodings.Web;
using System.Text.Json;
using BooleanCircuits.Models;

namespace BooleanCircuits.Helper.Serializable
{
    public class Serializer<T> where T : IModel
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        public Serializer() {}
        public Serializer(JsonSerializerOptions options) => _options = options;
        
        public string Serialize(ISerializable serializable) =>
            JsonSerializer.Serialize((T) serializable.Serialize(), _options);
    
        public string SerializeDefault(ISerializable serializable) =>
            JsonSerializer.Serialize((T) serializable.Serialize());
    }
}
