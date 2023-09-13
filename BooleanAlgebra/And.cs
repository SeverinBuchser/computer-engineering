namespace BooleanAlgebra
{
    public class And : DoubleEvaluator
    {
        public And(Evaluator evaluator1, Evaluator evaluator2) : base(evaluator1, evaluator2, "&") {}
        public override bool Evaluate(Values inputs) =>
            Evaluator1.Evaluate(inputs) & Evaluator2.Evaluate(inputs);
        public override bool Evaluate(Values inputs, InputMap map) =>
            Evaluator1.Evaluate(inputs, map) & Evaluator2.Evaluate(inputs, map);
    }
}
