using System;
using System.Collections.Generic;
using BooleanCircuits;
using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Nodes;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits
{
    public class CreatableCircuitTest {
        [Fact]
        public void Constructor_EmptyCreateAction_CreateActionCalledOnce()
        {
            Mock<Action<HashMap<GatewayNode>, HashMap<GatewayNode>>> mockCreate = new Mock<Action<HashMap<GatewayNode>, HashMap<GatewayNode>>>();
            CreatableCircuit _ = new CreatableCircuit("Name", "Id", mockCreate.Object);
            mockCreate.Verify(c => c(It.IsAny<HashMap<GatewayNode>>(), It.IsAny<HashMap<GatewayNode>>()), Times.Once());
        }
        
        [Fact]
        public void GetInput_AddInput_ReturnsInput()
        {
            Mock<GatewayNode> mockGatewayNode = new Mock<GatewayNode>("Input");
            mockGatewayNode.Setup(g => g.Hash()).Returns("Input");
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (inputs, _) =>
                inputs.Add(mockGatewayNode.Object));
            Assert.Equal(mockGatewayNode.Object, circuit.GetInput("Input"));
        }
    
        [Fact]
        public void GetInput_NoInput_ThrowsKeyNotFoundError()
        {
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (a, b) => {});
            Assert.Throws<KeyNotFoundException>(() => circuit.GetInput(""));
        }
        
        [Fact]
        public void GetOutput_AddOutput_ReturnsInput()
        {
            Mock<GatewayNode> mockGatewayNode = new Mock<GatewayNode>("Output");
            mockGatewayNode.Setup(g => g.Hash()).Returns("Output");
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (_, outputs) =>
                outputs.Add(mockGatewayNode.Object));
            Assert.Equal(mockGatewayNode.Object, circuit.GetOutput("Output"));
        }
        
        [Fact]
        public void GetOutput_NoOutput_ThrowsKeyNotFoundError()
        {
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (a, b) => {});
            Assert.Throws<KeyNotFoundException>(() => circuit.GetOutput(""));
        }
        
        [Fact]
        public void ContainsInput_NoInput_ReturnsFalse()
        {
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (a, b) => {});
            Assert.False(circuit.ContainsInput(""));
        }
        
        [Fact]
        public void ContainsInput_OneInput_ReturnsTrue()
        {
            Mock<GatewayNode> mockGatewayNode = new Mock<GatewayNode>("Input");
            mockGatewayNode.Setup(g => g.Hash()).Returns("Input");
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (inputs, _) =>
                inputs.Add(mockGatewayNode.Object));
            Assert.True(circuit.ContainsInput("Input"));
        }
        
        [Fact]
        public void ContainsOutput_NoOutput_ReturnsFalse()
        {
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (a, b) => {});
            Assert.False(circuit.ContainsOutput(""));
        }
        
        [Fact]
        public void ContainsOutput_OneOutput_ReturnsTrue()
        {
            Mock<GatewayNode> mockGatewayNode = new Mock<GatewayNode>("Output");
            mockGatewayNode.Setup(g => g.Hash()).Returns("Output");
            CreatableCircuit circuit = new CreatableCircuit("Name", "Id", (_, outputs) =>
                outputs.Add(mockGatewayNode.Object));
            Assert.True(circuit.ContainsOutput("Output"));
        }
    }
}
