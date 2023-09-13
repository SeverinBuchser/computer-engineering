using BooleanCircuits.Models;

namespace BooleanCircuits.Helper.Serializable
{
    public interface ISerializable
    {
        IModel Serialize();
    }
}
