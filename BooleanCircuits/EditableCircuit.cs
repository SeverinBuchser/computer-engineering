using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Models;
using BooleanCircuits.Nodes;

namespace BooleanCircuits
{
    public class EditableCircuit : Circuit
    {
        public new readonly HashMap<GatewayNode> Inputs = new HashMap<GatewayNode>();
        public new readonly HashMap<GatewayNode> Outputs = new HashMap<GatewayNode>();
        public readonly ObservableHashMap<Circuit> Circuits = new ObservableHashMap<Circuit>();
        private readonly HashMap<Connection> _connections = new HashMap<Connection>();
    
        public EditableCircuit(string name) : base(name, "THIS")
        {
            Circuits.Subscribe(circuit => _connections.ForEach(connection =>
                {
                    if (connection.Hash().Contains(circuit.Id))
                    {
                        connection.Dispose();
                        _connections.Remove(connection);
                    }
                })
            );
        }
    
        public override GatewayNode GetInput(string name) => Inputs.Get(name);
        public override GatewayNode GetOutput(string name) => Outputs.Get(name);
    
        public void Connect(
            string emitterId, string emitterName,
            string receiverId, string receiverName)
        {
            var emitterCircuit = emitterId == Id ? this : Circuits.Get(emitterId);
            GatewayNode emitter = emitterId == Id ? GetInput(emitterName) : emitterCircuit.GetOutput(emitterName);

            var receiverCircuit = receiverId == Id ? this : Circuits.Get(receiverId);
            GatewayNode receiver = receiverId == Id ? GetOutput(receiverName) : receiverCircuit.GetInput(receiverName);
    
            _connections.Add(new Connection(emitterCircuit, emitter, receiverCircuit, receiver));
        }
    
        public void Disconnect(string emitterId, string emitterName,
            string receiverId, string receiverName)
        {
            string hash = Connection.ConnectionHash(emitterId, emitterName, receiverId, receiverName);
            if (_connections.Contains(hash))
            {
                _connections.Get(hash).Dispose();
                _connections.Remove(hash);
            }
        }
    
        public override bool ContainsInput(string name) => Inputs.Contains(name);
        public override bool ContainsOutput(string name) => Outputs.Contains(name);
    
        public bool ContainsConnection(string emitterId, string emitterName,
            string receiverId, string receiverName) => _connections.Contains(
            Connection.ConnectionHash(emitterId, emitterName, receiverId, receiverName));
    
        public override IModel Serialize()
        {
            return new CreatableCircuitModel
            {
                Name = Name,
                Inputs = Inputs.Map(input => (NodeModel) input.Serialize()),
                Outputs = Outputs.Map(output => (NodeModel) output.Serialize()),
                Circuits = Circuits.Map(circuit => (CircuitModel) circuit.Serialize()),
                Connections = _connections.Map(connection => (ConnectionModel) connection.Serialize()),
            };
        }
    }
}

