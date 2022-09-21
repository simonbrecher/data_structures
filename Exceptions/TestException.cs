using System;

namespace Test
{
    // throw this exception on testing
    class TestException : Exception
    {
        public TestException(string message): base(message) {
            
        }
    }
}