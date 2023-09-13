using System;
using BooleanAlgebra;
using Xunit;

namespace Tests.BooleanAlgebra
{
    public class ParserTest {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Parse_Variable_ReturnCorrectEvaluator(bool aValue)
        {
            string formula = "a";
            Evaluator evaluator = Parser.Parse(formula);
            Values values = new Values {{"a", aValue}};
            Assert.Equal(aValue, evaluator.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void Parse_NOT_ReturnCorrectEvaluator(bool aValue, bool result)
        {
            string formula = "!a";
            Evaluator evaluator = Parser.Parse(formula);
            Values values = new Values {{"a", aValue}};
            Assert.Equal(result, evaluator.Evaluate(values));
        }
    
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, true)]
        public void Parse_AND_ReturnCorrectEvaluator(bool aValue, bool bValue, bool result)
        {
            string formula = "a & b";
            Evaluator evaluator = Parser.Parse(formula);
            Values values = new Values {{"a", aValue}, {"b", bValue}};
            Assert.Equal(result, evaluator.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void Parse_OR_ReturnCorrectEvaluator(bool aValue, bool bValue, bool result)
        {
            string formula = "a | b";
            Evaluator evaluator = Parser.Parse(formula);
            Values values = new Values {{"a", aValue}, {"b", bValue}};
            Assert.Equal(result, evaluator.Evaluate(values));
        }
    
        [Theory]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, false)]
        public void Parse_NOR_ReturnCorrectEvaluator(bool aValue, bool bValue, bool result)
        {
            string formula = "!a & !b";
            Evaluator evaluator = Parser.Parse(formula);
            Values values = new Values {{"a", aValue}, {"b", bValue}};
            Assert.Equal(result, evaluator.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void Parse_NAND_ReturnCorrectEvaluator(bool aValue, bool bValue, bool result)
        {
            string nandFormula = "!(a & b)";
            Evaluator nand = Parser.Parse(nandFormula);
            Values values = new Values {{"a", aValue}, {"b", bValue}};
            Assert.Equal(result, nand.Evaluate(values));
        }
        
        /**
         * a b c    a | c   a & (!b)  !(a & (!b))   result
         * 0 0 0    0       0       1           0
         * 0 0 1    1       0       1           1
         * 0 1 0    0       0       1           0
         * 0 1 1    1       0       1           1
         * 1 0 0    1       1       0           0
         * 1 0 1    1       1       0           0
         * 1 1 0    1       0       1           1
         * 1 1 1    1       0       1           1
         */
        [Theory]
        [InlineData(false, false, false, false)]
        [InlineData(false, false, true, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, true, true, true)]
        [InlineData(true, false, false, false)]
        [InlineData(true, false, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, true, true)]
        public void Parse_CustomFormula_ReturnCorrectEvaluator(bool aValue, bool bValue, bool cValue, bool result)
        {
            string nandFormula = "(!(a & (!b))) & (a | c)";
            Evaluator nand = Parser.Parse(nandFormula);
            Values values = new Values {{"a", aValue}, {"b", bValue}, {"c", cValue}};
            Assert.Equal(result, nand.Evaluate(values));
        }
    
        /**
             * a b S       
             * 0 0 0     
             * 0 1 1      
             * 1 0 1     
             * 1 1 0      
             */
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void Parse_XOr_ReturnCorrectEvaluator(bool aValue, bool bValue, bool result)
        {
            string xOrFormula = "((!a) & b) | (a & (!b))";
            Evaluator xOr = Parser.Parse(xOrFormula);
            Values values = new Values {{"a", aValue}, {"b", bValue}};
            Assert.Equal(result, xOr.Evaluate(values));
        }
        
        /**
         * a b cIn  S       cOut
         * 0 0 0    0       0      
         * 0 0 1    1       0     
         * 0 1 0    1       0      
         * 0 1 1    0       1     
         * 1 0 0    1       0  
         * 1 0 1    0       1     
         * 1 1 0    0       1      
         * 1 1 1    1       1      
         */
        [Theory]
        [InlineData(false, false, false, false, false)]
        [InlineData(false, false, true, true, false)]
        [InlineData(false, true, false, true, false)]
        [InlineData(false, true, true, false, true)]
        [InlineData(true, false, false, true, false)]
        [InlineData(true, false, true, false, true)]
        [InlineData(true, true, false, false, true)]
        [InlineData(true, true, true, true, true)]
        public void Parse_FullOneBitAdder_ReturnCorrectEvaluator(bool aValue, bool bValue, bool cIn, bool sumResult, bool cOutResult)
        {
            string iFormula = "(a & b)";
            string jFormula = "(a | b)";
            string cOutFormula = "(" + iFormula + " | (" + jFormula + " & cIn))";
            string sumFormula = "((" + jFormula + " | cIn) & (!" + cOutFormula + ")) | (" + iFormula + " & cIn)";
            Evaluator sum = Parser.Parse(sumFormula);
            Evaluator cOut = Parser.Parse(cOutFormula);
            Values values = new Values {{"a", aValue}, {"b", bValue}, {"cIn", cIn}};
            Assert.Equal(sumResult, sum.Evaluate(values));
            Assert.Equal(cOutResult, cOut.Evaluate(values));
        }
        
        [Theory]
        [InlineData("a")]
        [InlineData("(a)")]
        [InlineData("((((a))))")]
        [InlineData("   ab   \n")]
        public void Parse_CorrectFormula_DoesNotThrow(string formula)
        {
            Parser.Parse(formula);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(".")]
        [InlineData("~")]
        [InlineData("a & b)")]
        [InlineData("(a & b")]
        [InlineData("a & ")]
        [InlineData("&")]
        [InlineData("|")]
        [InlineData("!")]
        [InlineData("a |& b")]
        [InlineData("a & -")]
        [InlineData("(a) & (b) & (c)")]
        [InlineData("(!(a & (!b))) & (a | c")]
        [InlineData("(!(a & (!!))) & (a | c)")]
        [InlineData("(!(a & (!))) & (a | c)")]
        public void Parse_WrongFormula_ThrowsException(string formula)
        {
            Assert.Throws<Exception>(() => Parser.Parse(formula));
        }
    }
}
