using System;

using TestException = Test.TestException;

namespace TestDictionary
{
    // Specifies what methods are called on TestWrapper. (TestWrapper includes both BinaryTree and TestBinaryTree.)
    class Tests
    {
        // Run all tests x1000
        // Print messages
        public static void TestAll() {
            Console.WriteLine("Testing Dictionary");
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
                Console.WriteLine("Ended testing Dictionary without any errors.");
            } catch (TestException e) {
                Console.WriteLine("\n        ERROR: " + e.Message);
                Console.WriteLine("TEST DICTIONARY : ENDED WITH ERROR.");
            }
        }

        // Add keys in ascending order with random values
        private static void Test1(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            Tester tester = new Tester(min, max);
            tester.Start("Add ASC, [" + min + ", " + max + "):", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            Random random = new Random();
            
            tester.Test();
            for (int i = min; i < max; i++) {
                wrapper.Add(i, random.Next(min, max));
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add random keys and values
        private static void Test2(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 400;

            Tester tester = new Tester(min, max);
            tester.Start("Add, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            Random random = new Random();
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                wrapper.Add(random.Next(min, max), random.Next(min, max));
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add random keys and value, then remove random keys
        private static void Test3(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int sizeAdd = 200;
            int sizeRemove = 200;

            Tester tester = new Tester(min, max);
            tester.Start("Add -> Remove, [" + min + ", " + max + ") x" + sizeAdd + " x" + sizeRemove + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            Random random = new Random();
            
            tester.Test();
            for (int i = 0; i < sizeAdd; i++) {
                wrapper.Add(random.Next(min, max), random.Next(min, max));
                tester.Test();
            }

            for (int i = 0; i < sizeRemove; i++) {
                wrapper.Remove(random.Next(min, max));
                tester.Test();
            }

            tester.End(repetitionNumber, repetitionCount);
        }

        // Add random keys and values and remove random keys at the same time
        private static void Test4(int repetitionNumber, int repetitionCount) {
            int min = -100;
            int max = 100;

            int size = 800;

            Tester tester = new Tester(min, max);
            tester.Start("Add + Remove, [" + min + ", " + max + ") x" + size + ":", repetitionNumber);

            TestWrapper wrapper = new TestWrapper();
            tester.Watch(wrapper);

            Random random = new Random();
            
            tester.Test();
            for (int i = 0; i < size; i++) {
                if (random.Next(0, 2) == 0) {
                    wrapper.Add(random.Next(min, max), random.Next(min, max));
                    tester.Test();
                } else {
                    wrapper.Remove(random.Next(min, max));
                    tester.Test();
                }
            }

            tester.End(repetitionNumber, repetitionCount);
        }
    }
}