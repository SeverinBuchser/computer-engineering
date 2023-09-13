using System.Collections.Generic;
using BooleanCircuits;
using BooleanCircuits.Nodes;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits
{
    public class CircuitTest {
        [Theory]
        [InlineData("Circuit", "Circuit123134")]
        [InlineData("Name", "ID")]
        public void ToString_SetNameAndId_ReturnsNameColonId(string name, string id)
        {
            Mock<Circuit> circuit = new Mock<Circuit>(name, id)
            {
                CallBase = true
            };
            
            Assert.Equal(name + ":" + id, circuit.Object.ToString());
        }
        
        [Fact]
        public void GetInput_AddInput_ReturnsInput()
        {
            BasicCircuit circuit = new BasicCircuit();
            GatewayNode node = new GatewayNode("Node");
            circuit.AddInput(node);
            Assert.Equal(node, circuit.GetInput("Node"));
        }

        [Fact]
        public void GetInput_NoInput_ThrowsKeyNotFoundError()
        {
            BasicCircuit circuit = new BasicCircuit();
            Assert.Throws<KeyNotFoundException>(() => circuit.GetInput(""));
        }
        
        [Fact]
        public void GetOutput_AddOutput_ReturnsInput()
        {
            BasicCircuit circuit = new BasicCircuit();
            GatewayNode node = new GatewayNode("Node");
            circuit.AddOutput(node);
            Assert.Equal(node, circuit.GetOutput("Node"));
        }
        
        [Fact]
        public void GetOutput_NoOutput_ThrowsKeyNotFoundError()
        {
            BasicCircuit circuit = new BasicCircuit();
            Assert.Throws<KeyNotFoundException>(() => circuit.GetOutput(""));
        }
        
        [Fact]
        public void ContainsInput_NoInput_ReturnsFalse()
        {
            BasicCircuit circuit = new BasicCircuit();
            Assert.False(circuit.ContainsInput(""));
        }
        
        [Fact]
        public void ContainsInput_OneInput_ReturnsTrue()
        {
            BasicCircuit circuit = new BasicCircuit();
            GatewayNode node = new GatewayNode("Node");
            circuit.AddInput(node);
            Assert.True(circuit.ContainsInput("Node"));
        }
        
        [Fact]
        public void ContainsOutput_NoOutput_ReturnsFalse()
        {
            BasicCircuit circuit = new BasicCircuit();
            Assert.False(circuit.ContainsOutput(""));
        }
        
        [Fact]
        public void ContainsOutput_OneOutput_ReturnsTrue()
        {
            BasicCircuit circuit = new BasicCircuit();
            GatewayNode node = new GatewayNode("Node");
            circuit.AddOutput(node);
            Assert.True(circuit.ContainsOutput("Node"));
        }
    }
    
    public class BasicCircuit : Circuit
    {
        public BasicCircuit() : base("Basic", "Id") {}
        public void AddInput(GatewayNode node) => Inputs.Add(node);
        public void AddOutput(GatewayNode node) => Outputs.Add(node);
    }
    
}
