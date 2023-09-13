using BooleanAlgebra;
using Xunit;
using Moq;

namespace Tests.BooleanAlgebra
{
    public class VariableTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Evaluate_VarInInputs_ReturnsInput(bool value)
        {
            Variable var = new Variable("a");
            Values values = new Values {{"a", value}};
            Assert.Equal(value, var.Evaluate(values));
        }
        
        [Fact]
        public void Evaluate_VarNotInInputs_ReturnsFalse()
        {
            Variable var = new Variable("a");
            Assert.False(var.Evaluate(new Values()));
        }
    
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Evaluate_VarNotInInputs_ReturnsDefaultInputsValue(bool defaultInputsValue)
        {
            Variable var = new Variable("a");
            Values values = new Values(defaultInputsValue);
            Assert.Equal(defaultInputsValue, var.Evaluate(values));
        }
        
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Evaluate_MapVarToEvaluator_ReturnsInput(bool value)
        {
            Variable var = new Variable("a");
            Mock<Evaluator> mockEvaluator = new Mock<Evaluator>();
            Values values = new Values();
            InputMap map = new InputMap {{"a", mockEvaluator.Object}};
            mockEvaluator.Setup(e => e.Evaluate(values, map)).Returns(value);
            Assert.Equal(value, var.Evaluate(values, map));
        }
    
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Evaluate_TryMapVar_ReturnsEvaluationOfVar(bool value)
        {
            Variable var = new Variable("a");
            Values values = new Values {{"a", value}};
            InputMap map = new InputMap();
            Assert.Equal(value, var.Evaluate(values, map));
        }
    
        [Fact]
        public void ToString_Variable_ReturnsName()
        {
            Variable var = new Variable("a");
            Assert.Equal("a", var.ToString());
        }
    }
}
