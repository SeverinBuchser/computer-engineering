using BooleanAlgebra;
using Xunit;
using Moq;

namespace Tests.BooleanAlgebra
{
    public class NotTest
    {    
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void Evaluate_Evaluator_ReturnsNotOfEvaluator(bool aValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Values values = new Values();
            a.Setup(e => e.Evaluate(values)).Returns(aValue);
            
            Not not = new Not(a.Object);
            Assert.Equal(result, not.Evaluate(values));
        }
    
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void Evaluate_MapVariableToEvaluator_ReturnsNotOfEvaluator(bool value, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Variable b = new Variable("b");
            Values values = new Values();
            InputMap map = new InputMap {{"b", a.Object}};
            a.Setup(e => e.Evaluate(values, map)).Returns(value);
    
            Not not = new Not(b);
            Assert.Equal(result, not.Evaluate(values, map));
        }
        
        
        [Fact]
        public void ToString_Evaluators_ReturnsCorrectString()
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            a.Setup(e => e.ToString()).Returns("a");
    
            Not not = new Not(a.Object);
            Assert.Equal("!(a)", not.ToString());
        }
    
        [Fact]
        public void ToString_Variable_ReturnsCorrectString()
        {
            Variable a = new Variable("a");
    
            Not not = new Not(a);
            Assert.Equal("!a", not.ToString());
        }
    }
}
