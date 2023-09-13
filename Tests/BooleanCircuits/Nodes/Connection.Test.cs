using BooleanCircuits;
using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Nodes;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Nodes
{
    public class ConnectionTest {
        [Fact]
        public void Dispose_ConnectionWithEmitterAndReceiver_ReceiverRemovedFromEmitter()
        {
            Mock<Circuit> emitterCircuit = CreateCircuit("EmitterCircuit");
            Mock<IHashableEmitter> emitter = CreateEmitter("Emitter");
            Mock<Circuit> receiverCircuit = CreateCircuit("ReceiverCircuit");
            Mock<IHashableReceiver> receiver = CreateReceiver("Receiver");        
            Connection connection = new Connection(emitterCircuit.Object, emitter.Object,
                receiverCircuit.Object, receiver.Object);
            emitter.Verify(e => e.AddReceiver(receiver.Object), Times.Once());
            connection.Dispose();
            emitter.Verify(e => e.RemoveReceiver(receiver.Object), Times.Once());
        }
        
        [Fact]
        public void Hash_ConnectionWithEmitterAndReceiver_CorrectHash()
        {
            Mock<Circuit> emitterCircuit = CreateCircuit("EmitterCircuit");
            Mock<IHashableEmitter> emitter = CreateEmitter("Emitter");
            Mock<Circuit> receiverCircuit = CreateCircuit("ReceiverCircuit");
            Mock<IHashableReceiver> receiver = CreateReceiver("Receiver");        
            Connection connection = new Connection(emitterCircuit.Object, emitter.Object,
                receiverCircuit.Object, receiver.Object);
            
            Assert.Equal("EmitterCircuit:Emitter>ReceiverCircuit:Receiver", connection.Hash());
        }
    
        private Mock<IHashableEmitter> CreateEmitter(string hash)
        {
            Mock<IHashableEmitter> emitter = new Mock<IHashableEmitter>();
            emitter.Setup(e => e.Hash()).Returns(hash);
            return emitter;
        }
    
        private Mock<IHashableReceiver> CreateReceiver(string hash)
        {
            Mock<IHashableReceiver> receiver = new Mock<IHashableReceiver>();
            receiver.Setup(h => h.Hash()).Returns(hash);
            return receiver;
        }
    
        private Mock<Circuit> CreateCircuit(string hash)
        {
            Mock<Circuit> circuit = new Mock<Circuit>("Circuit", hash)
            {
                CallBase = true
            };
            return circuit;
        }
    }
}
