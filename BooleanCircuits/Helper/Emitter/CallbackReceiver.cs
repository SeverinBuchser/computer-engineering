using System;

namespace BooleanCircuits.Helper.Emitter
{
    public class CallbackReceiver : IReceiver
    {
        private readonly Action<bool> _callback;
        public CallbackReceiver(Action<bool> callback) => _callback = callback;
        public void Receive(bool value) => _callback(value);
    }
}
