namespace BooleanCircuits.Helper.HashMap
{
    public abstract class Hashable : IHashable {
        public abstract string Hash();
        
    
        public static string ConnectHash(string firstHash, string secondHash) => firstHash + ">" + secondHash;
        public static string ExtendHash(string extension, string hash) => extension + ":" + hash;
    }
}
