using System.Collections.Generic;

namespace BooleanAlgebra
{
    public class Values : Dictionary<string, bool>
    {
        private readonly bool _defaultValue;
        
        public Values() : this(false) {}
        public Values(bool defaultValue)
        {
            _defaultValue = defaultValue;
        }
    
        public bool GetOrDefault(Variable var) => GetOrDefault(var.Name);
        private bool GetOrDefault(string var)
        {
            if (!ContainsKey(var)) return _defaultValue;
            return this[var];
        }
    }
}
