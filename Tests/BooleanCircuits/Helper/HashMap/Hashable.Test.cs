using BooleanCircuits.Helper.HashMap;
using Xunit;

namespace Tests.BooleanCircuits.Helper.HashMap
{
    public class HashableTest {
        [Fact]
        public void ConnectHash_TwoStrings_ReturnsFirstStringArrowSecondString()
        {
            Assert.Equal("A>B", Hashable.ConnectHash("A", "B"));
        }
        
        [Fact]
        public void ExtendHash_TwoStrings_ReturnsFirstStringColonSecondString()
        {
            Assert.Equal("A:B", Hashable.ExtendHash("A", "B"));
        }
    }
}
