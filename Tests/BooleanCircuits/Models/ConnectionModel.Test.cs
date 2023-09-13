using System.Text.Json;
using BooleanCircuits.Models;
using Xunit;

namespace Tests.BooleanCircuits.Models
{
    public class ConnectionModelTest
    {
        [Fact]
        public void Validate_EmptyModel_ThrowsException()
        {
            ConnectionModel model = new ConnectionModel();
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Emitter Circuit cannot be empty!", e.Message);
        }
        
        [Fact]
        public void Validate_NoEmitterCircuit_ThrowsException()
        {
            ConnectionModel model = new ConnectionModel
            {
                EmitterCircuit = "",
                Emitter = "Emitter",
                ReceiverCircuit = "ReceiverCircuit",
                Receiver = "Receiver"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Emitter Circuit cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_NoEmitter_ThrowsException()
        {
            ConnectionModel model = new ConnectionModel
            {
                EmitterCircuit = "EmitterCircuit",
                Emitter = "",
                ReceiverCircuit = "ReceiverCircuit",
                Receiver = "Receiver"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Emitter cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_NoReceiverCircuit_ThrowsException()
        {
            ConnectionModel model = new ConnectionModel
            {
                EmitterCircuit = "EmitterCircuit",
                Emitter = "Emitter",
                ReceiverCircuit = "",
                Receiver = "Receiver"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Receiver Circuit cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_NoReceiver_ThrowsException()
        {
            ConnectionModel model = new ConnectionModel
            {
                EmitterCircuit = "EmitterCircuit",
                Emitter = "Emitter",
                ReceiverCircuit = "ReceiverCircuit",
                Receiver = ""
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Receiver cannot be empty!", e.Message);
        }
    }
}
