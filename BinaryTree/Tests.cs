using System;

using TestException = Test.TestException;

namespace TestBinaryTree
{
    // Specifies what methods are called on TestWrapper. (TestWrapper includes both BinaryTree and TestBinaryTree.)
    class Tests
    {
        // Run all tests x1000
        // Print messages
        public static void TestAll() {
            Console.WriteLine("Testing BinaryTree");
            try {
                int repetition = 1000;
                for (int i = 0; i < repetition; i++) {
                    Test1(i, repetition);
                }
                for (int i = 0; i < repetition; i++) {
                    Test2(i, repetition);
                }
                for (int i = 0; i < repetition; i++) {
                    Test3(i, repetition);
                }
                for (int i = 0; i < repetition; i++) {
                    Test4(i, repetition);
                }
                for (int i = 0; i < repetition; i++) {
                    Test5(i, repetition);
                }
                Console.WriteLine("Ended testing BinaryTree without any errors.");
            } catch (TestException e) {
                Console.WriteLine("\n        ERROR: " + e.Message);
                Console.WriteLine("TEST BINARY TREE : ENDED WITH ERROR.");
            }
        }

        // Add in ascending order
        private static void Test1(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            Tester tester = new Tester(min, max);
            tester.Start("Add ASC, [" + min + ", " + max + "):", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);
            
            tester.Test();
            for (int i = min; i < max; i++) {
                wrapper.Add(i);
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add random numbers
        private static void Test2(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 200;

            List<int> values = new List<int>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
            }

            Tester tester = new Tester(min, max);
            tester.Start("Add, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                wrapper.Add(values[i]);
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add random numbers, then remove random numbers
        private static void Test3(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int sizeAdd = 150;
            int sizeRemove = 300;

            List<int> values = new List<int>();

            Random random = new Random();
            for (int i = 0; i < sizeAdd + sizeRemove; i++) {
                values.Add(random.Next(min, max));
            }

            Tester tester = new Tester(min, max);
            tester.Start("Add -> Remove, [" + min + ", " + max + ") x" + sizeAdd + " x" + sizeRemove + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);
            
            tester.Test();
            for (int i = 0; i < sizeAdd; i++) {
                wrapper.Add(values[i]);
                tester.Test();
            }
            for (int i = 0; i < sizeRemove; i++) {
                wrapper.Add(values[sizeAdd + i]);
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add and Remove random numbers at the same time
        private static void Test4(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 500;

            List<int> values = new List<int>();
            List<bool> isAdd = new List<bool>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
                isAdd.Add(random.Next(0, 1) == 1);
            }

            Tester tester = new Tester(min, max);
            tester.Start("Add + Remove, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                if (isAdd[i]) {
                    wrapper.Add(values[i]);
                } else {
                    wrapper.Remove(values[i]);
                }
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add and Remove random numbers at the same time, but only numbers between 0 and 10
        private static void Test5(int repetitionNumber, int repetitionCount) {
            int min = 0;
            int max = 10;

            int size = 10000;

            List<int> values = new List<int>();
            List<bool> isAdd = new List<bool>();

            Random random = new Random();
            for (int i = 0; i < size; i++) {
                values.Add(random.Next(min, max));
                isAdd.Add(random.Next(0, 1) == 1);
            }

            Tester tester = new Tester(min, max);
            tester.Start("Add + Remove, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                if (isAdd[i]) {
                    wrapper.Add(values[i]);
                } else {
                    wrapper.Remove(values[i]);
                }
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }
    }
}