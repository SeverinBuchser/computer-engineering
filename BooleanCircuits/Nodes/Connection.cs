using System;
using BooleanCircuits.Helper.Emitter;
using BooleanCircuits.Helper.HashMap;
using BooleanCircuits.Helper.Serializable;
using BooleanCircuits.Models;

namespace BooleanCircuits.Nodes
{
    public class Connection : IDisposable, IHashable, ISerializable
    {
        private readonly Circuit _emitterCircuit;
        private readonly IHashableEmitter _emitter;
        private readonly Circuit _receiverCircuit;
        private readonly IHashableReceiver _receiver;
    
        public Connection(Circuit emitterCircuit, IHashableEmitter emitter, Circuit receiverCircuit, IHashableReceiver receiver)
        {
            _emitterCircuit = emitterCircuit;
            _emitter = emitter;
            _receiverCircuit = receiverCircuit;
            _receiver = receiver;
            emitter.AddReceiver(receiver);
        }
        
        public void Dispose() => _emitter.RemoveReceiver(_receiver);
        public string Hash() => ConnectionHash(_emitterCircuit.Hash(), _emitter.Hash(),
            _receiverCircuit.Hash(), _receiver.Hash());
        
        public static string ConnectionHash(string emitterCircuitHash, string emitterHash,
            string receiverCircuitHash, string receiverHash) => Hashable.ConnectHash(
            Hashable.ExtendHash(emitterCircuitHash, emitterHash),
            Hashable.ExtendHash(receiverCircuitHash, receiverHash));
    
        public IModel Serialize()
        {
            return new ConnectionModel
            {
                EmitterCircuit = _emitterCircuit.Hash(),
                Emitter = _emitter.Hash(),
                ReceiverCircuit = _receiverCircuit.Hash(),
                Receiver = _receiver.Hash(),
            };
        }
    }
}
