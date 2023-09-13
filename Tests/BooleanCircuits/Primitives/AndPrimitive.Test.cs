using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Primitives;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Primitives
{
    public class AndPrimitiveTest {
        [Fact]
        public void Functionality()
        {
            AndPrimitive and = new AndPrimitive("AND1");
            IReceiver input1 = and.GetInput("Input 1");
            IReceiver input2 = and.GetInput("Input 2");
            IEmitter output = and.GetOutput("Output");
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
            
            receiverMock.Verify(r => r.Receive(true), Times.Exactly(4));
            receiverMock.Verify(r => r.Receive(false), Times.Exactly(4));
        }
    }
}
