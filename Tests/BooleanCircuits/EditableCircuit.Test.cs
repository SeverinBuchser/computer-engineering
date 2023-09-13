using System;
using System.Collections.Generic;
using BooleanCircuits;
using BooleanCircuits.Models;
using BooleanCircuits.Nodes;
using BooleanCircuits.Primitives;
using Xunit;

namespace Tests.BooleanCircuits
{
    public class EditableCircuitTest
    {
        [Fact]
        public void GetInput_AddInput_ReturnsInput()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            GatewayNode node = new GatewayNode("Node");
            circuit.Inputs.Add(node);
            Assert.Equal(node, circuit.GetInput("Node"));
        }
    
        [Fact]
        public void GetInput_NoInput_ThrowsKeyNotFoundError()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            Assert.Throws<KeyNotFoundException>(() => circuit.GetInput(""));
        }
    
        [Fact]
        public void GetOutput_AddOutput_ReturnsInput()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            GatewayNode node = new GatewayNode("Node");
            circuit.Outputs.Add(node);
            Assert.Equal(node, circuit.GetOutput("Node"));
        }
    
        [Fact]
        public void GetOutput_NoOutput_ThrowsKeyNotFoundError()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            Assert.Throws<KeyNotFoundException>(() => circuit.GetOutput(""));
        }
    
        [Fact]
        public void Connect_ConnectOneInputToOneOutput_ObserveInputAtOutput()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "THIS", "Output 1");
            Assert.False(circuit.Outputs.Get("Output 1").Value);
            circuit.Inputs.Get("Input 1").Value = true;
            Assert.True(circuit.Outputs.Get("Output 1").Value);
        }
    
        [Fact]
        public void Disconnect_DisconnectOneInputToOneOutput_NoChangeAtOutput()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "THIS", "Output 1");
            circuit.Inputs.Get("Input 1").Value = true;
            Assert.True(circuit.Outputs.Get("Output 1").Value);
            circuit.Disconnect("THIS", "Input 1", "THIS", "Output 1");
            circuit.Inputs.Get("Input 1").Value = false;
            Assert.True(circuit.Outputs.Get("Output 1").Value);
        }
    
        [Fact]
        public void Connect_ConnectTwoInputsToTwoOutputs_ObserveInputsAtOutputs()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "THIS", "Output 1");
            circuit.Connect("THIS", "Input 2", "THIS", "Output 2");
    
            Assert.False(circuit.Outputs.Get("Output 1").Value);
            circuit.Inputs.Get("Input 1").Value = true;
            Assert.True(circuit.Outputs.Get("Output 1").Value);
    
            Assert.False(circuit.Outputs.Get("Output 2").Value);
            circuit.Inputs.Get("Input 2").Value = true;
            Assert.True(circuit.Outputs.Get("Output 2").Value);
        }
    
        [Fact]
        public void Connect_Everything_EveryInputAtEveryOutput()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "THIS", "Output 1");
            circuit.Connect("THIS", "Input 1", "THIS", "Output 2");
            circuit.Connect("THIS", "Input 2", "THIS", "Output 1");
            circuit.Connect("THIS", "Input 2", "THIS", "Output 2");
    
            Assert.True(circuit.ContainsConnection("THIS", "Input 1", "THIS", "Output 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 1", "THIS", "Output 2"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "THIS", "Output 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "THIS", "Output 2"));
    
            GatewayNode input1 = circuit.GetInput("Input 1");
            GatewayNode input2 = circuit.GetInput("Input 2");
            GatewayNode output1 = circuit.GetOutput("Output 1");
            GatewayNode output2 = circuit.GetOutput("Output 2");
    
            Assert.False(output1.Value);
            Assert.False(output2.Value);
    
            input1.Value = true;
            Assert.True(output1.Value);
            Assert.True(output2.Value);
    
            input2.Value = true;
            Assert.True(output1.Value);
            Assert.True(output2.Value);
    
            // TODO:
            // should not change, since the first singal is still true
            input2.Value = false;
            Assert.False(output1.Value);
            Assert.False(output2.Value);
        }
    
        [Fact]
        public void Connect_MakeSameConnectionTwice_DoesNotThrow()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "THIS", "Output 1");
            Assert.Throws<ArgumentException>(() => circuit.Connect("THIS", "Input 1", "THIS", "Output 1"));
        }
    
        [Fact]
        public void Connect_ConnectInputToNotAndNotToOutput_ObserveInvertedInputAtOutput()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            circuit.Connect("THIS", "Input 1", "NOT1", "Input");
            circuit.Connect("NOT1", "Output", "THIS", "Output 1");
    
            GatewayNode input1 = circuit.GetInput("Input 1");
            GatewayNode output1 = circuit.GetOutput("Output 1");
    
            Assert.True(output1.Value);
            input1.Value = true;
            Assert.False(output1.Value);
        }
    
        [Fact]
        public void Connect_CreateNANDGate_ObserveCorrectOutputs()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            // o1 = !(i1 & i2) (NAND gate)
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "NOT1", "Input");
            circuit.Connect("NOT1", "Output", "THIS", "Output 1");
    
            GatewayNode input1 = circuit.GetInput("Input 1");
            GatewayNode input2 = circuit.GetInput("Input 2");
            GatewayNode output1 = circuit.GetOutput("Output 1");
    
            // !(0 & 0) = !0 = 1 (0 0 1)
            Assert.False(input1.Value);
            Assert.False(input2.Value);
            Assert.True(output1.Value);
    
            // !(0 & 1) = !0 = 1 (0 1 1)
            circuit.Inputs.Get("Input 2").Value = true;
            Assert.False(input1.Value);
            Assert.True(input2.Value);
            Assert.True(output1.Value);
    
            // !(1 & 0) = !0 = 1 (1 0 1)
            circuit.Inputs.Get("Input 1").Value = true;
            circuit.Inputs.Get("Input 2").Value = false;
            Assert.True(input1.Value);
            Assert.False(input2.Value);
            Assert.True(output1.Value);
    
            // !(1 & 1) = !1 = 0 (1 1 0)
            circuit.Inputs.Get("Input 2").Value = true;
            Assert.True(input1.Value);
            Assert.True(input2.Value);
            Assert.False(output1.Value);
        }
    
        [Fact]
        public void Connect_ConnectNonExistingCircuit_ThrowsException()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            Assert.Throws<KeyNotFoundException>(() => circuit.Connect("A", "B", "C", "D"));
        }
    
        [Fact]
        public void ContainsConnection_CreateNANDGate_CheckConnectionSignatures()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            // o1 = !(i1 & i2) (NAND gate)
    
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "NOT1", "Input");
            circuit.Connect("NOT1", "Output", "THIS", "Output 1");
    
            Assert.True(circuit.ContainsConnection("THIS", "Input 1", "AND1", "Input 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "AND1", "Input 2"));
            Assert.True(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            Assert.True(circuit.ContainsConnection("NOT1", "Output", "THIS", "Output 1"));
        }
    
        [Fact]
        public void Disconnect_CreateNANDGate_NoOutputChangeAfterDisconnect()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            // o1 = !(i1 & i2) (NAND gate)
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "NOT1", "Input");
            circuit.Connect("NOT1", "Output", "THIS", "Output 1");
            circuit.Connect("THIS", "Input 2", "THIS", "Output 2");
    
            Assert.True(circuit.ContainsConnection("THIS", "Input 1", "AND1", "Input 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "AND1", "Input 2"));
            Assert.True(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            Assert.True(circuit.ContainsConnection("NOT1", "Output", "THIS", "Output 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "THIS", "Output 2"));
    
            circuit.Disconnect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Disconnect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Disconnect("AND1", "Output", "NOT1", "Input");
            circuit.Disconnect("NOT1", "Output", "THIS", "Output 1");
            circuit.Disconnect("THIS", "Input 2", "THIS", "Output 2");
    
            Assert.False(circuit.ContainsConnection("THIS", "Input 1", "AND1", "Input 1"));
            Assert.False(circuit.ContainsConnection("THIS", "Input 2", "AND1", "Input 2"));
            Assert.False(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            Assert.False(circuit.ContainsConnection("NOT1", "Output", "THIS", "Output 1"));
            Assert.False(circuit.ContainsConnection("THIS", "Input 2", "THIS", "Output 2"));
    
    
            circuit.Connect("AND1", "Output", "NOT1", "Input");
            Assert.True(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            circuit.Disconnect("AND1", "Output", "NOT1", "Input");
            Assert.False(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
        }
    
        [Fact]
        public void RemoveCircuit_RemoveOneCircuit_RemovesAllConnectionsWithCircuit()
        {
            EditableCircuit circuit = CreateBasicCircuit();
            // o1 = !(i1 & i2) (NAND gate)
    
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "NOT1", "Input");
            circuit.Connect("NOT1", "Output", "THIS", "Output 1");
    
            Assert.True(circuit.ContainsConnection("THIS", "Input 1", "AND1", "Input 1"));
            Assert.True(circuit.ContainsConnection("THIS", "Input 2", "AND1", "Input 2"));
            Assert.True(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            Assert.True(circuit.ContainsConnection("NOT1", "Output", "THIS", "Output 1"));
    
            // remove the and gate
            circuit.Circuits.Remove("AND1");
    
            Assert.False(circuit.ContainsConnection("THIS", "Input 1", "AND1", "Input 1"));
            Assert.False(circuit.ContainsConnection("THIS", "Input 2", "AND1", "Input 2"));
            Assert.False(circuit.ContainsConnection("AND1", "Output", "NOT1", "Input"));
            Assert.True(circuit.ContainsConnection("NOT1", "Output", "THIS", "Output 1"));
    
            // assert no change at output, since not connected
            GatewayNode input1 = circuit.GetInput("Input 1");
            GatewayNode input2 = circuit.GetInput("Input 2");
            GatewayNode output1 = circuit.GetOutput("Output 1");
    
            Assert.False(input1.Value);
            Assert.False(input2.Value);
            Assert.True(output1.Value);
    
            circuit.Inputs.Get("Input 2").Value = true;
            Assert.False(input1.Value);
            Assert.True(input2.Value);
            Assert.True(output1.Value);
    
            circuit.Inputs.Get("Input 1").Value = true;
            circuit.Inputs.Get("Input 2").Value = false;
            Assert.True(input1.Value);
            Assert.False(input2.Value);
            Assert.True(output1.Value);
    
            circuit.Inputs.Get("Input 2").Value = true;
            Assert.True(input1.Value);
            Assert.True(input2.Value);
            Assert.True(output1.Value);
        }
    
        [Fact]
        public void ContainsInput_NoInput_ReturnsFalse()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            Assert.False(circuit.ContainsInput(""));
        }
    
        [Fact]
        public void ContainsInput_OneInput_ReturnsTrue()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            GatewayNode node = new GatewayNode("Node");
            circuit.Inputs.Add(node);
            Assert.True(circuit.ContainsInput("Node"));
        }
    
        [Fact]
        public void ContainsOutput_NoOutput_ReturnsFalse()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            Assert.False(circuit.ContainsOutput(""));
        }
    
        [Fact]
        public void ContainsOutput_OneOutput_ReturnsTrue()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            GatewayNode node = new GatewayNode("Node");
            circuit.Outputs.Add(node);
            Assert.True(circuit.ContainsOutput("Node"));
        }
    
        [Fact]
        public void Serialize_AND_ReturnsJsonOfAND()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            circuit.Inputs.Add(new GatewayNode("Input 1"));
            circuit.Inputs.Add(new GatewayNode("Input 2"));
            circuit.Outputs.Add(new GatewayNode("Output"));
            circuit.Circuits.Add(new AndPrimitive("AND1"));
    
            circuit.Connect("THIS", "Input 1", "AND1", "Input 1");
            circuit.Connect("THIS", "Input 2", "AND1", "Input 2");
            circuit.Connect("AND1", "Output", "THIS", "Output");
    
            CreatableCircuitModel model = (CreatableCircuitModel) circuit.Serialize();
    
            Assert.Equal("Name", model.Name);
    
            // inputs
            Assert.Equal(2, model.Inputs.Count);
            Assert.Equal("Input 1", model.Inputs[0].Name);
            Assert.Equal("Input 2", model.Inputs[1].Name);
    
            // outputs
            Assert.Single(model.Outputs);
            Assert.Equal("Output", model.Outputs[0].Name);
    
            // circuits
            Assert.Single(model.Circuits);
            Assert.Equal("AND", model.Circuits[0].Name);
            Assert.Equal("AND1", model.Circuits[0].Id);
    
            // connections
            Assert.Equal(3, model.Connections.Count);
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
            Assert.Equal("THIS", model.Connections[2].ReceiverCircuit);
            Assert.Equal("Output", model.Connections[2].Receiver);
        }
    
        private EditableCircuit CreateBasicCircuit()
        {
            EditableCircuit circuit = new EditableCircuit("Name");
            circuit.Inputs.Add(new GatewayNode("Input 1"));
            circuit.Inputs.Add(new GatewayNode("Input 2"));
            circuit.Outputs.Add(new GatewayNode("Output 1"));
            circuit.Outputs.Add(new GatewayNode("Output 2"));
            circuit.Circuits.Add(new AndPrimitive("AND1"));
            circuit.Circuits.Add(new NotPrimitive("NOT1"));
            return circuit;
        }
    }
}
