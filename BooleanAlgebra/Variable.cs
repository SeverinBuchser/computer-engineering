namespace BooleanAlgebra
{
    public class Variable: Evaluator
    {
        public readonly string Name;
        public Variable(string name) => Name = name;    
        public override bool Evaluate(Values inputs) => inputs.GetOrDefault(this);
        public override bool Evaluate(Values inputs, InputMap map)
        {
            if (map.ContainsVariable(this)) return map.Map(this).Evaluate(inputs, map);
            return Evaluate(inputs);
        }

        public override string ToString() => Name;
    }
}

