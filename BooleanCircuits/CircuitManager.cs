using System;
using System.Collections.Generic;
using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Models;
using BooleanCircuits.Nodes;
using BooleanCircuits.Primitives;

namespace BooleanCircuits
{
    public class CircuitManager
    {
        private static readonly Dictionary<string, Func<string, Circuit>> PrimitiveCircuits = new Dictionary<string, Func<string, Circuit>>
        {
            { "AND", id => new AndPrimitive(id) },
            { "OR", id => new OrPrimitive(id) },
            { "NOT", id => new NotPrimitive(id) }
        };
    
        private static readonly
            Dictionary<string, Action<HashMap<GatewayNode>, HashMap<GatewayNode>>>
            Creators = new Dictionary<string, Action<HashMap<GatewayNode>, HashMap<GatewayNode>>>();
        
        public static List<string> GetAllCircuits()
        {
            List<string> all = new List<string>();
            foreach (string key in PrimitiveCircuits.Keys) all.Add(key);
            foreach (string key in Creators.Keys) all.Add(key);
            return all;
        }
        
        public static void RegisterCreatableCircuit(List<CreatableCircuitModel> models) =>
            models.ForEach(RegisterCreatableCircuit);
    
        public static void RegisterCreatableCircuit(CreatableCircuitModel model)
        {
            Creators.Add(model.Name, (inputs, outputs) =>
            {
                CreateGatewayNodes(inputs, model.Inputs);
                CreateGatewayNodes(outputs, model.Outputs);
                CreateConnections(inputs, outputs, CreateCircuits(model.Circuits), model.Connections);
            });
        }
        
        public static void Clear() => Creators.Clear();
    
        private static void CreateGatewayNodes(HashMap<GatewayNode> inputs, List<NodeModel> models) =>
            models.ForEach(model => inputs.Add(new GatewayNode(model.Name)));
    
        private static HashMap<Circuit> CreateCircuits(List<CircuitModel> models)
        {
            HashMap<Circuit> circuits = new HashMap<Circuit>();
            models.ForEach(model => circuits.Add(CreateCircuit(model)));
            return circuits;
        }
    
        public static Circuit CreateCircuit(CircuitModel model)
        {
            if (IsPrimitiveCircuit(model))
                return PrimitiveCircuits[model.Name](model.Id);
            if (!Creators.ContainsKey(model.Name))
                throw new Exception($"Circuit {model.Name} is not registered!");
            return new CreatableCircuit(model.Name, model.Id, Creators[model.Name]);
        }
    
        private static void CreateConnections(HashMap<GatewayNode> inputs, HashMap<GatewayNode> outputs,
            HashMap<Circuit> circuits, List<ConnectionModel> models)
        {
            models.ForEach(model => CreateConnection(inputs, outputs, circuits, model));
        }
        
        private static void CreateConnection(HashMap<GatewayNode> inputs, HashMap<GatewayNode> outputs,
            HashMap<Circuit> circuits, ConnectionModel model)
        {
            IEmitter emitter = model.EmitterCircuit == "THIS"
                ? inputs.Get(model.Emitter)
                : circuits.Get(model.EmitterCircuit).GetOutput(model.Emitter);
            IReceiver receiver = model.ReceiverCircuit == "THIS"
                ? outputs.Get(model.Receiver)
                : circuits.Get(model.ReceiverCircuit).GetInput(model.Receiver);

            emitter.AddReceiver(receiver);
        }
    
        private static bool IsPrimitiveCircuit(CircuitModel model) =>
            model.Name == "AND" || model.Name == "OR" || model.Name == "NOT";
    }
}
