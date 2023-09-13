using BooleanCircuits.Nodes;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Nodes
{
    public class NodeTest {    
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, true)]
        public void AndOperator_TwoNodes_ReturnsCorrectBinaryAnd(bool value1, bool value2, bool result)
        {
            Mock<Node> node1 = GetMockNode(value1);
            Mock<Node> node2 = GetMockNode(value2);
            
            Assert.Equal(result, node1.Object & node2.Object);
        }
        
    
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void OrOperator_TwoNodes_ReturnsCorrectBinaryOr(bool value1, bool value2, bool result)
        {
            Mock<Node> node1 = GetMockNode(value1);
            Mock<Node> node2 = GetMockNode(value2);
        
            Assert.Equal(result, node1.Object | node2.Object);
        }
        
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void NotOperator_OneNode_ReturnsCorrectBinaryNot(bool value, bool result)
        {
            Mock<Node> node = GetMockNode(value);
            
            Assert.Equal(result, !node.Object);
        }
        
        private Mock<Node> GetMockNode(bool value)
        {
            return new Mock<Node>("Node", value)
            {
                CallBase = true
            };
        }
    }
}
