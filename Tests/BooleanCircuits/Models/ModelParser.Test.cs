using System.Collections.Generic;
using System.Text.Json;
using BooleanCircuits.Models;
using Tests.Utility;
using Xunit;

namespace Tests.BooleanCircuits.Models
{
    public class ModelParserTest {
        [Theory]
        [FileData("Resources/NAND.json")]
        public void Parse_NAND_ReturnsCreatableCircuitModel(string json)
        {
            CreatableCircuitModel model = ModelParser.Parse<CreatableCircuitModel>(json);
            
            Assert.Equal("NAND", model.Name);
            
            // inputs
            Assert.Equal(2, model.Inputs.Count);
            Assert.Equal("Input 1", model.Inputs[0].Name);
            Assert.Equal("Input 2", model.Inputs[1].Name);
            
            // outputs
            Assert.Single(model.Outputs);
            Assert.Equal("Output", model.Outputs[0].Name);
            
            // circuits
            Assert.Equal(2, model.Circuits.Count);
            Assert.Equal("AND", model.Circuits[0].Name);
            Assert.Equal("AND1", model.Circuits[0].Id);
            Assert.Equal("NOT", model.Circuits[1].Name);
            Assert.Equal("NOT1", model.Circuits[1].Id);
            
            // connections
            Assert.Equal(4, model.Connections.Count);
            Assert.Equal("THIS", model.Connections[0].EmitterCircuit);
            Assert.Equal("Input 1", model.Connections[0].Emitter);
            Assert.Equal("AND1", model.Connections[0].ReceiverCircuit);
            Assert.Equal("Input 1", model.Connections[0].Receiver);
            
            Assert.Equal("THIS", model.Connections[1].EmitterCircuit);
            Assert.Equal("Input 2", model.Connections[1].Emitter);
            Assert.Equal("AND1", model.Connections[1].ReceiverCircuit);
            Assert.Equal("Input 2", model.Connections[1].Receiver);
    
            Assert.Equal("AND1", model.Connections[2].EmitterCircuit);
            Assert.Equal("Output", model.Connections[2].Emitter);
            Assert.Equal("NOT1", model.Connections[2].ReceiverCircuit);
            Assert.Equal("Input", model.Connections[2].Receiver);
            
            Assert.Equal("NOT1", model.Connections[3].EmitterCircuit);
            Assert.Equal("Output", model.Connections[3].Emitter);
            Assert.Equal("THIS", model.Connections[3].ReceiverCircuit);
            Assert.Equal("Output", model.Connections[3].Receiver);
        }
    
        [Fact]
        public void Parse_WrongStringForModel_ThrowsException()
        {
            string json = "{\"SomeProperty\": \"value\"}";
            Assert.Throws<JsonException>(() => ModelParser.Parse<CreatableCircuitModel>(json));
        }
        
        [Theory]
        [FileData("Resources/Circuits.json")]
        public void Parse_NANDAndNOR_ReturnsCreatableCircuitModels(string json)
        {
            List<CreatableCircuitModel> models = ModelParser.ParseAll<CreatableCircuitModel>(json);

            // NAND
            CreatableCircuitModel nand = models[0];
            
            Assert.Equal("NAND", nand.Name);
            
            // inputs
            Assert.Equal(2, nand.Inputs.Count);
            Assert.Equal("Input 1", nand.Inputs[0].Name);
            Assert.Equal("Input 2", nand.Inputs[1].Name);
            
            // outputs
            Assert.Single(nand.Outputs);
            Assert.Equal("Output", nand.Outputs[0].Name);
            
            // circuits
            Assert.Equal(2, nand.Circuits.Count);
            Assert.Equal("AND", nand.Circuits[0].Name);
            Assert.Equal("AND1", nand.Circuits[0].Id);
            Assert.Equal("NOT", nand.Circuits[1].Name);
            Assert.Equal("NOT1", nand.Circuits[1].Id);
            
            // connections
            Assert.Equal(4, nand.Connections.Count);
            Assert.Equal("THIS", nand.Connections[0].EmitterCircuit);
            Assert.Equal("Input 1", nand.Connections[0].Emitter);
            Assert.Equal("AND1", nand.Connections[0].ReceiverCircuit);
            Assert.Equal("Input 1", nand.Connections[0].Receiver);
            
            Assert.Equal("THIS", nand.Connections[1].EmitterCircuit);
            Assert.Equal("Input 2", nand.Connections[1].Emitter);
            Assert.Equal("AND1", nand.Connections[1].ReceiverCircuit);
            Assert.Equal("Input 2", nand.Connections[1].Receiver);
            
            Assert.Equal("AND1", nand.Connections[2].EmitterCircuit);
            Assert.Equal("Output", nand.Connections[2].Emitter);
            Assert.Equal("NOT1", nand.Connections[2].ReceiverCircuit);
            Assert.Equal("Input", nand.Connections[2].Receiver);
            
            Assert.Equal("NOT1", nand.Connections[3].EmitterCircuit);
            Assert.Equal("Output", nand.Connections[3].Emitter);
            Assert.Equal("THIS", nand.Connections[3].ReceiverCircuit);
            Assert.Equal("Output", nand.Connections[3].Receiver);
            
            
            // NOR
            CreatableCircuitModel nor = models[1];

            Assert.Equal("NOR", nor.Name);
            
            // inputs
            Assert.Equal(2, nor.Inputs.Count);
            Assert.Equal("Input 1", nor.Inputs[0].Name);
            Assert.Equal("Input 2", nor.Inputs[1].Name);
            
            // outputs
            Assert.Single(nor.Outputs);
            Assert.Equal("Output", nor.Outputs[0].Name);
            
            // circuits
            Assert.Equal(2, nor.Circuits.Count);
            Assert.Equal("OR", nor.Circuits[0].Name);
            Assert.Equal("OR1", nor.Circuits[0].Id);
            Assert.Equal("NOT", nor.Circuits[1].Name);
            Assert.Equal("NOT1", nor.Circuits[1].Id);
            
            // connections
            Assert.Equal(4, nor.Connections.Count);
            Assert.Equal("THIS", nor.Connections[0].EmitterCircuit);
            Assert.Equal("Input 1", nor.Connections[0].Emitter);
            Assert.Equal("OR1", nor.Connections[0].ReceiverCircuit);
            Assert.Equal("Input 1", nor.Connections[0].Receiver);
            
            Assert.Equal("THIS", nor.Connections[1].EmitterCircuit);
            Assert.Equal("Input 2", nor.Connections[1].Emitter);
            Assert.Equal("OR1", nor.Connections[1].ReceiverCircuit);
            Assert.Equal("Input 2", nor.Connections[1].Receiver);
            
            Assert.Equal("OR1", nor.Connections[2].EmitterCircuit);
            Assert.Equal("Output", nor.Connections[2].Emitter);
            Assert.Equal("NOT1", nor.Connections[2].ReceiverCircuit);
            Assert.Equal("Input", nor.Connections[2].Receiver);
            
            Assert.Equal("NOT1", nor.Connections[3].EmitterCircuit);
            Assert.Equal("Output", nor.Connections[3].Emitter);
            Assert.Equal("THIS", nor.Connections[3].ReceiverCircuit);
            Assert.Equal("Output", nor.Connections[3].Receiver);
        }

        [Fact]
        public void Parse_WrongStringForModels_ThrowsException()
        {
            string json = "[{\"SomeProperty\": \"value\"}, {\"OtherProperty\": \"otherValue\"}]";
            Assert.Throws<JsonException>(() => ModelParser.ParseAll<CreatableCircuitModel>(json));
        }
        
        [Fact]
        public void Parse_NotAJsonString_ThrowsException()
        {
            string json = "";
            Assert.Throws<JsonException>(() => ModelParser.Parse<IModel>(json));
        }
    }
}
