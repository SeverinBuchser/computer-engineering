namespace BooleanAlgebra
{
    public abstract class DoubleEvaluator : Evaluator
    {
        protected readonly Evaluator Evaluator1;
        protected readonly Evaluator Evaluator2;
        private readonly string _operatorString;
        protected DoubleEvaluator(Evaluator evaluator1, Evaluator evaluator2, string operatorString) =>
            (Evaluator1, Evaluator2, _operatorString) = (evaluator1, evaluator2, operatorString);
        public override string ToString()
        {
            string left = Evaluator1.ToString();
            string right = Evaluator2.ToString();
            if (!(Evaluator1 is Variable)) left = "(" + left + ")";
            if (!(Evaluator2 is Variable)) right = "(" + right + ")";
            return left + " " + _operatorString + " " + right;
        }
    }
}
