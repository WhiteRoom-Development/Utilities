using System;

namespace Utilities.Runtime
{
    public static class IDGenerator
    {
        private static readonly Random s_Random = new();


        /// <summary>
        /// Returns a string id intended for use in small, medium or large collections. <br></br>
        /// If you need a more global id which is almost guaranteed to be unique use System.Guid.
        /// </summary>
        public static string GenerateStringId()
        {
            var buffer = GetRandom(4);
            
            var id = Convert.ToBase64String(buffer);

            return id.Remove(id.Length - 3);
        }

        /// <summary>
        /// Returns an integer id intended for use in small or medium collections. <br></br>
        /// If you need a more global id which is almost guaranteed to be unique use System.Guid. <br></br>
        /// The id will have 7 digits max, so it can be stored as a float, if needed.
        /// </summary>
        public static int GenerateIntegerId()
        {
            return s_Random.Next(-999999999, 999999999);
        }

        private static byte[] GetRandom(int size)
        {
            var bytes = new byte[size];
            
            s_Random.NextBytes(bytes);

            return bytes;
        }
    }
}