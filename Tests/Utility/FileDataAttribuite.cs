using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Xunit.Sdk;

namespace Tests.Utility
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        public JsonFileDataAttribute(string filePath) => _filePath = filePath;
    
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
    
            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetFullPath(Directory.GetCurrentDirectory() + "/" + _filePath);
    
            if (!File.Exists(path)) throw new Exception($"Could not find file at path: {path}");
            var data = JsonSerializer.Deserialize(File.ReadAllText(_filePath), testMethod.GetParameters()[0].ParameterType);
            if (data == null) throw new Exception("Could not parse the json file");
            return new List<object[]> { new [] {data}};
        }
    }
}
