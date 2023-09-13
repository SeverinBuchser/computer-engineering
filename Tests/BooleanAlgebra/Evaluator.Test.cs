using BooleanAlgebra;
using Xunit;
using Moq;

namespace Tests.BooleanAlgebra
{
    public class EvaluatorTest
    {
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, true)]
        public void AndOperator_Evaluators_ReturnsAndOfEvaluators(bool aValue, bool bValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Mock<Evaluator> b = new Mock<Evaluator>();
            Values values = new Values();
            a.Setup(e => e.Evaluate(values)).Returns(aValue);
            b.Setup(e => e.Evaluate(values)).Returns(bValue);
    
            Evaluator and = a.Object & b.Object;
            Assert.True(and is And);
            Assert.Equal(result, and.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void OrOperator_Evaluators_ReturnsOrOfEvaluators(bool aValue, bool bValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Mock<Evaluator> b = new Mock<Evaluator>();
            Values values = new Values();
            a.Setup(e => e.Evaluate(values)).Returns(aValue);
            b.Setup(e => e.Evaluate(values)).Returns(bValue);
    
            Evaluator or = a.Object | b.Object;
            Assert.True(or is Or);
            Assert.Equal(result, or.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void NotOperator_Evaluator_ReturnsNotOfEvaluator(bool aValue, bool result)
        {
            Mock<Evaluator> a = new Mock<Evaluator>();
            Values values = new Values();
            a.Setup(e => e.Evaluate(values)).Returns(aValue);
    
            Evaluator not = !a.Object;
            Assert.True(not is Not);
            Assert.Equal(result, not.Evaluate(values));
        }
    }
}
