namespace BooleanAlgebra
{
    public class Not : Evaluator
    {
        private readonly Evaluator _evaluator;
        public Not(Evaluator evaluator) => _evaluator = evaluator;
        public override bool Evaluate(Values inputs) => !_evaluator.Evaluate(inputs);
        public override bool Evaluate(Values inputs, InputMap map) => !_evaluator.Evaluate(inputs, map);
    
        public override string ToString()
        {
            string center = _evaluator.ToString();
            if (!(_evaluator is Variable)) center = "(" + center + ")";
            return "!" + center;
        }
    }
}
