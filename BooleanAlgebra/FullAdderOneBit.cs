using System.Collections.Generic;

namespace BooleanAlgebra
{
    public class FullAdderOneBit : Dictionary<string, Evaluator>
    {
        public FullAdderOneBit()
        {
            Evaluator a = new Variable("a");
            Evaluator b = new Variable("b");
            Evaluator carryIn = new Variable("carryIn");
            Evaluator aXOrb = (!a & b) | (a & !b);
            this["carryOut"] = (carryIn & aXOrb) | (a & b);
            this["sum"] = (!aXOrb & carryIn) | (aXOrb & !carryIn);
        }
    }
}
