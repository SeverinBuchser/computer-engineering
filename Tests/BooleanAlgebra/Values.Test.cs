using BooleanAlgebra;
using Xunit;

namespace Tests.BooleanAlgebra
{
    public class InputsTest
    {
        [Theory]
        [InlineData("VarA")]
        [InlineData("VarB")]
        [InlineData("asdfkjasht2oukqwkjasg")]
        public void GetOrDefault_NoInputAdded_ReturnsFalse(string varName)
        {
            Values values = new Values();
            Assert.False(values.GetOrDefault(new Variable(varName)));
        }
        
        [Theory]
        [InlineData(false, "VarA")]
        [InlineData(false, "VarB")]
        [InlineData(false, "asdfkjasht2oukqwkjasg")]
        [InlineData(true, "VarA")]
        [InlineData(true, "VarB")]
        [InlineData(true, "asdfkjasht2oukqwkjasg")]
        public void GetOrDefault_NoInputAdded_ReturnsDefault(bool defaultInputsValue, string varName)
        {
            Values values = new Values(defaultInputsValue);
            Assert.Equal(defaultInputsValue, values.GetOrDefault(new Variable(varName)));
        }
    }
}
