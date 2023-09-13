using System.Collections.Generic;
using System.Text.Json;
using BooleanCircuits.Models;
using Xunit;

namespace Tests.BooleanCircuits.Models
{
    public class CreatableCircuitModelTest
    {
        [Fact]
        public void Validate_EmptyModel_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel();
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of circuit cannot be empty!", e.Message);
        }
        
        [Fact]
        public void Validate_NoName_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel();
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of circuit cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_EmptyInputList_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Circuit has to contain at least one input!", e.Message);
        }
    
        [Fact]
        public void Validate_InvalidInputNodeModel_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {new NodeModel()}
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of node cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_EmptyOutputList_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {_createNodeModel()}
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Circuit has to contain at least one output!", e.Message);
        }
    
        [Fact]
        public void Validate_InvalidOutputNodeModel_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {_createNodeModel()},
                Outputs = new List<NodeModel> {new NodeModel()}
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of node cannot be empty!", e.Message);
        }
    
        [Fact]
        public void Validate_EmptyConnectionsList_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {_createNodeModel()},
                Outputs = new List<NodeModel> {_createNodeModel()},
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Circuit has to contain at least one connection!", e.Message);
        }
        
        [Fact]
        public void Validate_InvalidConnectionModel_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {_createNodeModel()},
                Outputs = new List<NodeModel> {_createNodeModel()},
                Connections = new List<ConnectionModel> {new ConnectionModel()}
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Emitter Circuit cannot be empty!", e.Message);
        }
        
        [Fact]
        public void Validate_InvalidCircuitModel_ThrowsException()
        {
            CreatableCircuitModel model = new CreatableCircuitModel
            {
                Name = "Name",
                Inputs = new List<NodeModel> {_createNodeModel()},
                Outputs = new List<NodeModel> {_createNodeModel()},
                Connections = new List<ConnectionModel> {_createConnectionModel()},
                Circuits = new List<CircuitModel> {new CircuitModel()}
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of circuit cannot be empty!", e.Message);
        }
        
        private NodeModel _createNodeModel()
        {
            return new NodeModel
            {
                Name = "NodeName"
            };
        }
        
        private ConnectionModel _createConnectionModel()
        {
            return new ConnectionModel
            {
                EmitterCircuit = "EmitterCircuit",
                Emitter = "Emitter",
                ReceiverCircuit = "ReceiverCircuit",
                Receiver = "Recevier"
            };
        }
    }
}
