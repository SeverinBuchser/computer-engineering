using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Primitives;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Primitives
{
    public class OrPrimitiveTest {
        [Fact]
        public void Functionality()
        {
            OrPrimitive or = new OrPrimitive("OR1");
            IReceiver input1 = or.GetInput("Input 1");
            IReceiver input2 = or.GetInput("Input 2");
            IEmitter output = or.GetOutput("Output");
            Mock<IReceiver> receiverMock = new Mock<IReceiver>();
            output.AddReceiver(receiverMock.Object);
    
            input1.Receive(true);
            input2.Receive(true);
            
            input1.Receive(false);
            input2.Receive(false);
            
            input1.Receive(true);
            input2.Receive(true);
            
            input1.Receive(true);
            input2.Receive(false);
            
            input1.Receive(true);
            input2.Receive(true);
            
            input1.Receive(false);
            input2.Receive(true);
            
            input1.Receive(true);
            input2.Receive(true);
            
            receiverMock.Verify(r => r.Receive(true), Times.Exactly(2));
            receiverMock.Verify(r => r.Receive(false), Times.Exactly(2));
        }
    }
}
