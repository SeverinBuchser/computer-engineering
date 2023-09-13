using System.Collections.Generic;
using System.Text.Json;

namespace BooleanCircuits.Models
{
    public class ModelParser {
        public static T Parse<T>(string json) where T : IModel
        {
            var model = JsonSerializer.Deserialize<T>(json);
            model.Validate();
            return model;
        }
        
        public static List<T> ParseAll<T>(string json) where T : IModel
        {
            var models = JsonSerializer.Deserialize<List<T>>(json);
            models.ForEach(model => model.Validate());
            return models;
        }
    }
}
