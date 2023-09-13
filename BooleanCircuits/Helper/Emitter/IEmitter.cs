namespace BooleanCircuits.Helper.Emitter
{
    public interface IEmitter {
        void AddReceiver(IReceiver receiver);
        void RemoveReceiver(IReceiver receiver);
    }
}
