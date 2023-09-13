using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Nodes;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Nodes
{
    public class GatewayNodeTest {
        [Fact]
        public void AddReceiver_OneReceiver_ReceiverReceivesValues()
        {
            GatewayNode gateway = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);

            receiver.Verify(r => r.Receive(false), Times.Once());
            receiver.Verify(r => r.Receive(true), Times.Never());
        }
            
            
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AddReceiver_InitalValueOneReceiver_ReceiverReceivesValues(bool inital)
        {
            GatewayNode gateway = new GatewayNode("Emitter", inital);
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);
        
            receiver.Verify(r => r.Receive(inital), Times.Once());
            receiver.Verify(r => r.Receive(!inital), Times.Never());
        }
        
        
        [Fact]
        public void AddReceiver_AddSameReceiverMultipleTimes_NoEmissionAfterFirstAddition()
        {
            GatewayNode gateway = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);
            gateway.AddReceiver(receiver.Object);
            gateway.AddReceiver(receiver.Object);
            gateway.AddReceiver(receiver.Object);
            gateway.AddReceiver(receiver.Object);
        
            receiver.Verify(r => r.Receive(false), Times.Once());
            receiver.Verify(r => r.Receive(true), Times.Never());
        }
        
        [Fact]
        public void Emit_True_ReceiverReceivesInitalFalseAndTrue()
        {
            GatewayNode gateway = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);
        
            gateway.Value = true;
            receiver.Verify(r => r.Receive(false), Times.Once());
            receiver.Verify(r => r.Receive(true), Times.Once());
        }
        
        [Fact]
        public void RemoveReceiver_AddOnceRemoveOnce_NoEmissionAfterRemoval()
        {
            GatewayNode gateway = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);
            gateway.RemoveReceiver(receiver.Object);
        
            gateway.Value = true;
            receiver.Verify(r => r.Receive(false), Times.Once());
            receiver.Verify(r => r.Receive(true), Times.Never());
        }
        
        [Fact]
        public void RemoveReceiver_AddThenRemoveThenAddReceiver_TwoEmissionsOfInitalValue()
        {
            GatewayNode gateway = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            gateway.AddReceiver(receiver.Object);
            gateway.RemoveReceiver(receiver.Object);
            gateway.AddReceiver(receiver.Object);
        
            gateway.Value = true;
            receiver.Verify(r => r.Receive(false), Times.Exactly(2));
            receiver.Verify(r => r.Receive(true), Times.Once());
        }
        
        [Fact]
        public void RemoveReceiver_RemoveWithoutAdding_NoExceptionThrown()
        {
            GatewayNode emitter = new GatewayNode("Emitter");
            Mock<IReceiver> receiver = new Mock<IReceiver>();
            emitter.RemoveReceiver(receiver.Object);
        }
        
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Receive_Boolean_ValueIsBoolean(bool receive)
        {
            Mock<GatewayNode> node = new Mock<GatewayNode>("Node")
            {
                CallBase = true
            };
            node.Object.Receive(receive);
            Assert.Equal(receive, node.Object.Value);
        }
        
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Receive_Boolean_ValueIsBooleanRegardlessOfInitalValue(bool inital, bool receive)
        {
            Mock<GatewayNode> node = new Mock<GatewayNode>("Node", inital)
            {
                CallBase = true
            };
            node.Object.Receive(receive);
            Assert.Equal(receive, node.Object.Value);
        }
    }
}
