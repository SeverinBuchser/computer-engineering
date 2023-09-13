namespace BooleanAlgebra
{
    public abstract class Evaluator
    {
        public abstract bool Evaluate(Values inputs);
        public abstract bool Evaluate(Values inputs, InputMap map);
        
        public static Evaluator operator& (Evaluator evaluator1, Evaluator evaluator2) => new And(evaluator1, evaluator2);
        public static Evaluator operator| (Evaluator evaluator1, Evaluator evaluator2) => new Or(evaluator1, evaluator2);
        public static Evaluator operator! (Evaluator evaluator) => new Not(evaluator);
    }
}
