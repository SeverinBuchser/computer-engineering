using BooleanAlgebra;
using Xunit;
using Moq;

namespace Tests.BooleanAlgebra
{
    public class OrTest
    {    
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void Evaluate_Evaluators_ReturnsOrOfEvaluators(bool aValue, bool bValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Mock<Evaluator> b = new Mock<Evaluator>();
            Values values = new Values();
            a.Setup(e => e.Evaluate(values)).Returns(aValue);
            b.Setup(e => e.Evaluate(values)).Returns(bValue);
            
            Or or = new Or(a.Object, b.Object);
            Assert.Equal(result, or.Evaluate(values));
        }
    
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void Evaluate_MapVarToEvaluator_ReturnsOrOfEvaluators(bool aValue, bool bValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Mock<Evaluator> b = new Mock<Evaluator>();
            Variable c = new Variable("c");
            Values values = new Values();
            InputMap map = new InputMap {{"c", a.Object}};
            a.Setup(e => e.Evaluate(values, map)).Returns(aValue);
            b.Setup(e => e.Evaluate(values, map)).Returns(bValue);
        
            Or or = new Or(c, b.Object);
            Assert.Equal(result, or.Evaluate(values, map));
        }
    
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Evaluate_MapBothVariablesToSameEvaluator_ReturnsOrOfEvaluators(bool value)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Variable b = new Variable("b");
            Variable c = new Variable("c");
            Values values = new Values();
            InputMap map = new InputMap {{"b", a.Object}, {"c", a.Object}};
            a.Setup(e => e.Evaluate(values, map)).Returns(value);
    
            Or or = new Or(b, c);
            Assert.Equal(value, or.Evaluate(values, map));
        }
        
        
        [Fact]
        public void ToString_Evaluators_ReturnsCorrectString()
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Mock<Evaluator> b = new Mock<Evaluator>();
            a.Setup(e => e.ToString()).Returns("a");
            b.Setup(e => e.ToString()).Returns("b");
        
            Or or = new Or(a.Object, b.Object);
            Assert.Equal("(a) | (b)", or.ToString());
        }
    
        [Fact]
        public void ToString_Variables_ReturnsCorrectString()
        {
            Variable a = new Variable("a");
            Variable b = new Variable("b");
    
            Or or = new Or(a, b);
            Assert.Equal("a | b", or.ToString());
        }
    }
}
