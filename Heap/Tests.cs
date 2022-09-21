using System;

using HeapVertex = DataStructures.HeapVertex;
using TestException = Test.TestException;

namespace TestHeap
{
    // Specifies what methods are called on TestWrapper. (TestWrapper includes both Heap and TestHeap.)
    class Tests
    {
        // Run all tests x1000
        // Print messages
        public static void TestAll() {
            Console.WriteLine("Testing Heap");
            try {
                int repeat = 1000;
                for (int i = 0; i < repeat; i++) {
                    Test1(i, repeat);
                }
                for (int i = 0; i < repeat; i++) {
                    Test2(i, repeat);
                }
                for (int i = 0; i < repeat; i++) {
                    Test3(i, repeat);
                }
                for (int i = 0; i < repeat; i++) {
                    Test4(i, repeat);
                }
                for (int i = 0; i < repeat; i++) {
                    Test5(i, repeat);
                }
                Console.WriteLine("Ended testing Heap without any errors.");
            } catch (TestException e) {
                Console.WriteLine("\n        ERROR: " + e.Message);
                Console.WriteLine("TEST HEAP : ENDED WITH ERROR.");
            }
        }

        // Add -> RemoveMin
        private static void Test1(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 200;

            Tester tester = new Tester();
            tester.Start("Add -> RemoveMin, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            List<int> values = new List<int>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
            }
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                wrapper.Add(values[i], null);
                tester.Test();
            }

            for (int i = 0; i < size; i++) {
                wrapper.RemoveMin();
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add -> Remove -> RemoveMin
        private static void Test2(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 200;

            Tester tester = new Tester();
            tester.Start("Add -> Remove -> RemoveMin, [" + min + ", " + max + ") x" + size + " 50% :", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            List<int> values = new List<int>();
            List<bool> doRemove = new List<bool>();
            List<HeapVertex> vertices = new List<HeapVertex>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
                doRemove.Add(random.Next(0, 2) == 0);
            }
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                HeapVertex vertex = wrapper.Add(values[i], null);
                vertices.Add(vertex);
                tester.Test();
            }

            for (int i = 0; i < size; i++) {
                if (doRemove[i]) {
                    wrapper.Remove(vertices[i]);
                    tester.Test();
                }
            }

            while (wrapper.Heap.Size > 0) {
                wrapper.RemoveMin();
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add -> Change -> RemoveMin
        private static void Test3(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 200;

            Tester tester = new Tester();
            tester.Start("Add -> Change -> RemoveMin, [" + min + ", " + max + ") x" + size + " :", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            List<int> values = new List<int>();
            List<int> change = new List<int>();
            List<HeapVertex> vertices = new List<HeapVertex>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
                change.Add(random.Next(min, max));
            }
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                HeapVertex vertex = wrapper.Add(values[i], null);
                vertices.Add(vertex);
                tester.Test();
            }

            for (int i = 0; i < size; i++) {
                wrapper.Change(vertices[i], change[i]);
                tester.Test();
            }

            while (wrapper.Heap.Size > 0) {
                wrapper.RemoveMin();
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add -> Union -> RemoveMin
        private static void Test4(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 100;

            Tester tester = new Tester();
            tester.Start("Add -> Union -> RemoveMin, [" + min + ", " + max + ") x" + size + " :", repetitionNumber);

            TestWrapper wrapper1 = new TestWrapper();
            tester.Watch(wrapper1);
            TestWrapper wrapper2 = new TestWrapper();
            tester.Watch(wrapper2);

            List<int> values1 = new List<int>();
            List<int> values2 = new List<int>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values1.Add(random.Next(min, max));
                values2.Add(random.Next(min, max));
            }
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                wrapper1.Add(values1[i], null);
                tester.Test();
                wrapper2.Add(values2[i], null);
                tester.Test();
            }

            wrapper1.Union(wrapper2);
            tester.Test();

            while (wrapper1.Heap.Size > 0) {
                wrapper1.RemoveMin();
                tester.Test();
            }
            while (wrapper2.Heap.Size > 0) {
                wrapper2.RemoveMin();
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add -> Union (constructor) -> RemoveMin
        private static void Test5(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 70;

            Tester tester = new Tester();
            tester.Start("Add -> Union (constructor) -> RemoveMin, [" + min + ", " + max + ") x" + size + " :", repetitionNumber);

            TestWrapper wrapper1 = new TestWrapper();
            tester.Watch(wrapper1);
            TestWrapper wrapper2 = new TestWrapper();
            tester.Watch(wrapper2);

            List<int> values1 = new List<int>();
            List<int> values2 = new List<int>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values1.Add(random.Next(min, max));
                values2.Add(random.Next(min, max));
            }
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                wrapper1.Add(values1[i], null);
                tester.Test();
                wrapper2.Add(values2[i], null);
                tester.Test();
            }

            TestWrapper wrapper3 = new TestWrapper(wrapper1, wrapper2);
            tester.Watch(wrapper3);

            tester.Test();

            while (wrapper1.Heap.Size > 0) {
                wrapper1.RemoveMin();
                tester.Test();
            }
            while (wrapper2.Heap.Size > 0) {
                wrapper2.RemoveMin();
                tester.Test();
            }
            while (wrapper3.Heap.Size > 0) {
                wrapper3.RemoveMin();
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }
    }
}