using System;
using System.Collections.Generic;
using BooleanCircuits;
using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Models;
using Tests.Utility;
using Xunit;
using Moq;

namespace Tests.BooleanCircuits
{
        public class CircuitManagerTest : IDisposable {
            public void Dispose()
            {
                CircuitManager.Clear();
            }
            
            [Theory]
            [JsonFileData("Resources/Circuits.json")]
            public void RegisterCreatableCircuit_Models_RegistersCircuits(List<CreatableCircuitModel> models)
            {
                    CircuitManager.RegisterCreatableCircuit(models);
                    CircuitManager.CreateCircuit(models[0].ToCircuitModel("Id"));
                    CircuitManager.CreateCircuit(models[1].ToCircuitModel("Id"));
            }
            
            [Theory]
            [JsonFileData("Resources/NAND.json")]
            public void RegisterCreatableCircuit_NAND_RegistersCircuit(CreatableCircuitModel model)
            {
                    CircuitManager.RegisterCreatableCircuit(model);
                    CircuitManager.CreateCircuit(model.ToCircuitModel("Id"));
            }
                
            [Theory]
            [JsonFileData("Resources/NAND.json")]
            public void CreateCircuit_NAND_ReturnsCorrectCircuit(CreatableCircuitModel model) {
                    CircuitManager.RegisterCreatableCircuit(model);
                    model.Validate();
                    
                    Circuit circuit = CircuitManager.CreateCircuit(model.ToCircuitModel("Id"));
                        
                    IReceiver input1 = circuit.GetInput("Input 1");
                    IReceiver input2 = circuit.GetInput("Input 2");
                    IEmitter output = circuit.GetOutput("Output");
                        
                    Mock<IReceiver> mockReceiver = new Mock<IReceiver>();
                    output.AddReceiver(mockReceiver.Object);
                        
                    // !(0 & 0) = !0 = 1
                    mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        
                    // !(0 & 1) = !0 = 1
                    input2.Receive(true);
                    mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        
                    // !(1 & 0) = !0 = 1
                    // set input 2 to false first, else the output will change since both inputs would be true
                    input2.Receive(false);
                    input1.Receive(true);
                    mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        
                    // !(1 & 1) = !1 = 0
                    input2.Receive(true);
                    mockReceiver.Verify(r => r.Receive(true), Times.Once());
                    mockReceiver.Verify(r => r.Receive(false), Times.Once());
                }
            
                [Theory]
                [JsonFileData("Resources/NOR.json")]
                public void CreateCircuit_NOR_ReturnsCorrectCircuit(CreatableCircuitModel model) {
                        CircuitManager.RegisterCreatableCircuit(model);
                        model.Validate();
                        Circuit circuit = CircuitManager.CreateCircuit(model.ToCircuitModel("Id"));
                        
                        IReceiver input1 = circuit.GetInput("Input 1");
                        IReceiver input2 = circuit.GetInput("Input 2");
                        IEmitter output = circuit.GetOutput("Output");
                        
                        Mock<IReceiver> mockReceiver = new Mock<IReceiver>();
                        output.AddReceiver(mockReceiver.Object);
                        
                        // !(0 | 0) = !0 = 1 
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        
                        // !(0 | 1) = !1 = 0 
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // !(1 | 0) = !1 = 0 
                        // // set input 1 to true first, else the output will change since both inputs would be false
                        input1.Receive(true);
                        input2.Receive(false); 
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // !(1 | 1) = !1 = 0 
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                }
                
        
        
                [Fact]
                public void CreateCircuit_AND_ReturnsPrimitveANDCircuit() {
                        CircuitModel model = new CircuitModel()
                        {
                                Name = "AND",
                                Id = "AND1"
                        };
                        Circuit circuit = CircuitManager.CreateCircuit(model);
                
                        IReceiver input1 = circuit.GetInput("Input 1");
                        IReceiver input2 = circuit.GetInput("Input 2");
                        IEmitter output = circuit.GetOutput("Output");
                        
                        Mock<IReceiver> mockReceiver = new Mock<IReceiver>();
                        output.AddReceiver(mockReceiver.Object);
                        
                        // 0 & 0 = 0
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 0 & 1 = 0
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 1 & 0 = 0 
                        // // set input 2 to false first, else the output will change since both inputs would be true
                        input2.Receive(false); 
                        input1.Receive(true);
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 1 & 1 = 1 
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                }
        
        
        
                [Fact]
                public void CreateCircuit_OR_ReturnsPrimitveANDCircuit() {
                        CircuitModel model = new CircuitModel()
                        {
                                Name = "OR",
                                Id = "OR1"
                        };
                        Circuit circuit = CircuitManager.CreateCircuit(model);
                
                        IReceiver input1 = circuit.GetInput("Input 1");
                        IReceiver input2 = circuit.GetInput("Input 2");
                        IEmitter output = circuit.GetOutput("Output");
                        
                        Mock<IReceiver> mockReceiver = new Mock<IReceiver>();
                        output.AddReceiver(mockReceiver.Object);
                        
                        // 0 | 0 = 0
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 0 | 1 = 1
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 1 | 0 = 1 
                        // // set input 1 to true first, else the output will change since both inputs would be false
                        input1.Receive(true);
                        input2.Receive(false); 
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                        
                        // 1 | 1 = 1 
                        input2.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                }
                
                
                
                [Fact]
                public void CreateCircuit_NOT_ReturnsPrimitveANDCircuit() {
                        CircuitModel model = new CircuitModel()
                        {
                                Name = "NOT",
                                Id = "NOT1"
                        };
                        Circuit circuit = CircuitManager.CreateCircuit(model);
                
                        IReceiver input = circuit.GetInput("Input");
                        IEmitter output = circuit.GetOutput("Output");
                        
                        Mock<IReceiver> mockReceiver = new Mock<IReceiver>();
                        output.AddReceiver(mockReceiver.Object);
                        
                        // !0 = 1
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        
                        // !1 = 1
                        input.Receive(true);
                        mockReceiver.Verify(r => r.Receive(true), Times.Once());
                        mockReceiver.Verify(r => r.Receive(false), Times.Once());
                }
                
        
                
                [Fact]
                public void CreateCircuit_NonExistingCreatableCircuit_ThrowsException() {
                        CircuitModel model = new CircuitModel()
                        {
                                Name = "NotExisting",
                                Id = "NotExisting"
                        };
                        Assert.Throws<Exception>(() => CircuitManager.CreateCircuit(model));
                }
        }
}
