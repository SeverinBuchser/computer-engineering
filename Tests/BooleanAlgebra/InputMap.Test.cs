using System.Collections.Generic;
using BooleanAlgebra;
using Xunit;
using Moq;

namespace Tests.BooleanAlgebra
{
    public class InputMapTest
    {
        [Fact]
        public void ContainsVariable_OneVariable_ReturnsTrue()
        {
            InputMap map = new InputMap { { "a", new Mock<Evaluator>().Object } };
            Assert.True(map.ContainsVariable(new Variable("a")));
        }
        
        [Fact]
        public void Map_OneVariable_ReturnsEvaluator()
        {
            Mock<Evaluator> mockEvaluator = new Mock<Evaluator>();
            InputMap map = new InputMap { { "a", mockEvaluator.Object } };
            Assert.Equal(mockEvaluator.Object, map.Map(new Variable("a")));
        }
        
        [Fact]
        public void Map_NoVariable_ThrowsKeyNotFoundException()
        {
            InputMap map = new InputMap();
            Assert.Throws<KeyNotFoundException>(() => map.Map(new Variable("a")));
        }
    }
}
