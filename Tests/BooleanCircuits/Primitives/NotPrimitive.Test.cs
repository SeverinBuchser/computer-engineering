using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Primitives;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Primitives
{
    public class NotPrimitiveTest {
        [Fact]
        public void Functionality()
        {
            NotPrimitive not = new NotPrimitive("NOT1");
            IReceiver input = not.GetInput("Input");
            IEmitter output = not.GetOutput("Output");
            Mock<IReceiver> receiverMock = new Mock<IReceiver>();
            output.AddReceiver(receiverMock.Object);
    
            input.Receive(true);
            input.Receive(true);
            
            input.Receive(false);
            input.Receive(false);
            
            input.Receive(true);
            input.Receive(true);
            
            receiverMock.Verify(r => r.Receive(true), Times.Exactly(2));
            receiverMock.Verify(r => r.Receive(false), Times.Exactly(2));
        }
    }
}
