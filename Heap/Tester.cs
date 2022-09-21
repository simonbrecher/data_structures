using System;

using HeapVertex = DataStructures.HeapVertex;
using TestException = Test.TestException;

namespace TestHeap
{
    // This class checks, if Heap is in correct format and if the outputs are same as in TestHeap.
    // You can watch more than one TestWrapper.
    class Tester
    {
        private List<TestWrapper> Wrappers;

        // Does not specify key interval (like other 2 other Tester(s), because it uses RemoveMin() instead)
        public Tester() {
            Wrappers = new List<TestWrapper>();
        }

        // If you want to include a wrapper in testing, add it here.
        public void Watch(TestWrapper wrapper) {
            Wrappers.Add(wrapper);
        }

        // Test whole heap. (Not recursive)
        private void TestHeap(TestWrapper wrapper) {
            if (wrapper.Heap.Size != wrapper.Heap.Vertices.Count()) {
                throw new TestException("heap.Size: " + wrapper.Heap.Size + " != heap.Vertices.Count(): " + wrapper.Heap.Vertices.Count());
            }
            
            if (wrapper.Heap.Size != wrapper.Test.Size) {
                throw new TestException("heap.Size: " + wrapper.Heap.Size + " != test.Size: " + wrapper.Test.Size);
            }

            for (int i = 0; i < wrapper.Heap.Size; i++) {
                if (! wrapper.Heap.Vertices[i].IsAlive) {
                    throw new TestException("heap.Vertices[" + i + "].IsAlive == false");
                }
            }

            for (int i = 1; i < wrapper.Heap.Size; i++) {
                HeapVertex vertex = wrapper.Heap.Vertices[i];
                HeapVertex parent = wrapper.Heap.Vertices[(i - 1) / 2];
                
                if (vertex.Key < parent.Key) {
                    throw new TestException("heap.Vertices[" + i + "].Key: " + vertex.Key + " < heap.Vertices[" + (i - 1) / 2 + "].Key: " + parent.Key);
                }
            }

            if (wrapper.Heap.Size != 0) {
                int heapMin = wrapper.GetMin(true);
                int testMin = wrapper.GetMin(false);
                if (heapMin != testMin) {
                    throw new TestException("heap.GetMin(): " + heapMin + " != testHeap.GetMin(): " + testMin);
                }
            }
        }

        // Test all wrappers, which are being watched.
        public void Test() {
            foreach (TestWrapper wrapper in Wrappers) {
                TestHeap(wrapper);
            }
        }

        // Print test specifications, if it is first test. (Tests repeat each test x1000)
        public void Start(string message, int repetitionNumber) {
            if (repetitionNumber == 0) {
                Console.Write("    " + message.PadRight(65));
            }
        }

        // Print Success message, if it is the last test. (Tests repeat each test x1000)
        public void End(int repetitionNumber, int repetitionCount) {
            if (repetitionNumber == repetitionCount - 1) {
                Console.WriteLine("Success (x" + repetitionCount + ")");
            }
        }
    }
}