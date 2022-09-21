using System;

namespace DataStructures
{
    // throw this exception on everything, which is not only for testing
    class DataStructuresException : Exception
    {
        public DataStructuresException(string message): base(message) {
            
        }
    }
}