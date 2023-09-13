using BooleanAlgebra;
using Xunit;

namespace Tests.BooleanAlgebra
{
    public class FullAdderOneBitTest
    {
        [Theory]
        [InlineData(false, false, false, false, false)]
        [InlineData(false, false, true, true, false)]
        [InlineData(false, true, false, true, false)]
        [InlineData(false, true, true, false, true)]
        [InlineData(true, false, false, true, false)]
        [InlineData(true, false, true, false, true)]
        [InlineData(true, true, false, false, true)]
        [InlineData(true, true, true, true, true)]
        public void Adder_Input_ReturnsCorrectSum(bool aValue, bool bValue, bool carryInValue, bool sumResult, bool carryOutResult)
        {
            FullAdderOneBit adder = new FullAdderOneBit();
            Values values = new Values {{"a", aValue}, {"b", bValue}, {"carryIn", carryInValue}};
            Assert.Equal(sumResult, adder["sum"].Evaluate(values));
            Assert.Equal(carryOutResult, adder["carryOut"].Evaluate(values));
        }
    }
}
