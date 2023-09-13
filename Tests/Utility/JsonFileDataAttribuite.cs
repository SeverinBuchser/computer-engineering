using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace Tests.Utility
{
    public class FileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        public FileDataAttribute(string filePath) => _filePath = filePath;
    
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
    
            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetFullPath(Directory.GetCurrentDirectory() + "/" + _filePath);
    
            if (!File.Exists(path)) throw new Exception($"Could not find file at path: {path}");
            
            return new [] { new object[] {File.ReadAllText(_filePath) }};
        }
    }
}
