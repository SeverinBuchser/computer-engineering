using System.Collections.Generic;

namespace BooleanAlgebra
{
    public class InputMap : Dictionary<string, Evaluator>
    {
        public bool ContainsVariable(Variable variable) => ContainsKey(variable.Name);
        public Evaluator Map(Variable variable) => this[variable.Name];
    }
}
