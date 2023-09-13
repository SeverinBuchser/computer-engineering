using System.Text.Encodings.Web;
using System.Text.Json;
using BooleanCircuits;
using BooleanCircuits.Helper.Serializable;
using BooleanCircuits.Models;
using BooleanCircuits.Nodes;
using BooleanCircuits.Primitives;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits.Helper.Serializable
{
    public class SerializableTest
    {
        [Fact]
        public void Serialize_DefaultOptionsWithANDGate_ReturnsJsonOfAND()
        {
            EditableCircuit circuit = CreateCircuit();
            Serializer<CreatableCircuitModel> serializer = new Serializer<CreatableCircuitModel>();
            
            string json = @"{
  ""Name"": ""Name"",
  ""Inputs"": [
    {
      ""Name"": ""Input 1""
    },
    {
      ""Name"": ""Input 2""
    }
  ],
  ""Outputs"": [
    {
      ""Name"": ""Output""
    }
  ],
  ""Circuits"": [
    {
      ""Name"": ""AND"",
      ""Id"": ""AND1""
    }
  ],
  ""Connections"": [
    {
      ""EmitterCircuit"": ""THIS"",
      ""Emitter"": ""Input 1"",
      ""ReceiverCircuit"": ""AND1"",
      ""Receiver"": ""Input 1""
    },
    {
      ""EmitterCircuit"": ""THIS"",
      ""Emitter"": ""Input 2"",
      ""ReceiverCircuit"": ""AND1"",
      ""Receiver"": ""Input 2""
    },
    {
      ""EmitterCircuit"": ""AND1"",
      ""Emitter"": ""Output"",
      ""ReceiverCircuit"": ""THIS"",
      ""Receiver"": ""Output""
    }
  ]
}";
            
            Assert.Equal(json, serializer.Serialize(circuit));
        }
        
        [Fact]
        public void Serialize_DefaultOption_ReturnsJson()
        {
            Serializer<TestModel> serializer = new Serializer<TestModel>();
            Mock<ISerializable> mockSerializable = new Mock<ISerializable>();
            mockSerializable.Setup(s => s.Serialize()).Returns(new TestModel {Sth = "sth"});
            string json = "{\n" +
                "  \"Sth\": \"sth\"\n" +
                "}";
            Assert.Equal(json, serializer.Serialize(mockSerializable.Object));
        }
        
    
        [Fact]
        public void Serialize_WithGivenOptionsWithANDGate_ReturnsJsonOfAND()
        {
            EditableCircuit circuit = CreateCircuit();
            Serializer<CreatableCircuitModel> serializer = new Serializer<CreatableCircuitModel>(new JsonSerializerOptions());
        
            string json = @"{""Name"":""Name"",""Inputs"":[{""Name"":""Input 1""},{""Name"":""Input 2""}],""Outputs"":[{""Name"":""Output""}],""Circuits"":[{""Name"":""AND"",""Id"":""AND1""}],""Connections"":[{""EmitterCircuit"":""THIS"",""Emitter"":""Input 1"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 1""},{""EmitterCircuit"":""THIS"",""Emitter"":""Input 2"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 2""},{""EmitterCircuit"":""AND1"",""Emitter"":""Output"",""ReceiverCircuit"":""THIS"",""Receiver"":""Output""}]}";
        
            Assert.Equal(json, serializer.Serialize(circuit));
        }
    
        [Fact]
        public void Serialize_WithGivenOptions_ReturnsJson()
        {
            Serializer<TestModel> serializer = new Serializer<TestModel>(new JsonSerializerOptions());
            Mock<ISerializable> mockSerializable = new Mock<ISerializable>();
            mockSerializable.Setup(s => s.Serialize()).Returns(new TestModel {Sth = "sth"});
            string json = "{\"Sth\":\"sth\"}";
            Assert.Equal(json, serializer.Serialize(mockSerializable.Object));
        }
    
    
        [Fact]
        public void SerializeDefault_WithGivenOptionsWithANDGate_ReturnsJsonOfAND()
        {
            EditableCircuit circuit = CreateCircuit();
    
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            Serializer<CreatableCircuitModel> serializer = new Serializer<CreatableCircuitModel>(options);
            
            string json = @"{""Name"":""Name"",""Inputs"":[{""Name"":""Input 1""},{""Name"":""Input 2""}],""Outputs"":[{""Name"":""Output""}],""Circuits"":[{""Name"":""AND"",""Id"":""AND1""}],""Connections"":[{""EmitterCircuit"":""THIS"",""Emitter"":""Input 1"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 1""},{""EmitterCircuit"":""THIS"",""Emitter"":""Input 2"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 2""},{""EmitterCircuit"":""AND1"",""Emitter"":""Output"",""ReceiverCircuit"":""THIS"",""Receiver"":""Output""}]}";
            
            Assert.Equal(json, serializer.SerializeDefault(circuit));
        }
    
        [Fact]
        public void SerializeDefault_WithGivenOptions_ReturnsJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            Serializer<TestModel> serializer = new Serializer<TestModel>(options);
            
            Mock<ISerializable> mockSerializable = new Mock<ISerializable>();
            mockSerializable.Setup(s => s.Serialize()).Returns(new TestModel {Sth = "sth"});
            string json = "{\"Sth\":\"sth\"}";
            Assert.Equal(json, serializer.SerializeDefault(mockSerializable.Object));
        }
        
        [Fact]
        public void SerializeDefault_WithoutOptionsWithANDGate_ReturnsJsonOfAND()
        {
            EditableCircuit circuit = CreateCircuit();
            Serializer<CreatableCircuitModel> serializer = new Serializer<CreatableCircuitModel>();
    
            string json = @"{""Name"":""Name"",""Inputs"":[{""Name"":""Input 1""},{""Name"":""Input 2""}],""Outputs"":[{""Name"":""Output""}],""Circuits"":[{""Name"":""AND"",""Id"":""AND1""}],""Connections"":[{""EmitterCircuit"":""THIS"",""Emitter"":""Input 1"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 1""},{""EmitterCircuit"":""THIS"",""Emitter"":""Input 2"",""ReceiverCircuit"":""AND1"",""Receiver"":""Input 2""},{""EmitterCircuit"":""AND1"",""Emitter"":""Output"",""ReceiverCircuit"":""THIS"",""Receiver"":""Output""}]}";
            
            Assert.Equal(json, serializer.SerializeDefault(circuit));
        }
    
        [Fact]
        public void SerializeDefault_WithoutOptions_ReturnsJson()
        {
            Serializer<TestModel> serializer = new Serializer<TestModel>();
            Mock<ISerializable> mockSerializable = new Mock<ISerializable>();
            mockSerializable.Setup(s => s.Serialize()).Returns(new TestModel {Sth = "sth"});
            string json = "{\"Sth\":\"sth\"}";
            Assert.Equal(json, serializer.SerializeDefault(mockSerializable.Object));
        }
        
        private EditableCircuit CreateCircuit()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            circuit.Inputs.Add(new GatewayNode("Input 1"));
            circuit.Inputs.Add(new GatewayNode("Input 2"));
            circuit.Outputs.Add(new GatewayNode("Output"));
            circuit.Circuits.Add(new AndPrimitive("AND1"));
    
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "THIS", "Output");
            
            return circuit;
        }
    }
    
    public sealed class TestModel : IModel
    {
        public string Sth {get; set;} = "";
        public void Validate() {}
    }
}
