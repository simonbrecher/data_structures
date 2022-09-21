using System;

using Dictionary = DataStructures.Dictionary;
using TestException = Test.TestException;

namespace TestDictionary
{
    // This class checks, if Dictionary is in correct format and if the outputs are same as in TestDictionary.
    // You can watch more than one TestWrapper.
    class Tester
    {
        private List<TestWrapper> Wrappers;
        public int[] KeysInterval = new int[] {};

        // @parameter specify minimal and maximal value of key.
        public Tester(int hasMin, int hasMax) {
            Wrappers = new List<TestWrapper>();
            SetKeysInterval(hasMin, hasMax);
        }

        // Create array with all possible keys. Used in constructor
        public void SetKeysInterval(int hasMin, int hasMax) {
            KeysInterval = new int[] {};
            for (int i = hasMin; i < hasMax; i++) {
                KeysInterval.Append(i);
            }
        }

        // If you want to include a wrapper in testing, add it here.
        public void Watch(TestWrapper wrapper) {
            Wrappers.Add(wrapper);
        }

        // Test whole dictionary. (Not recursive)
        private void TestDictionary(TestWrapper wrapper) {
            Dictionary dictionary = wrapper.Dictionary;

            if (dictionary.Capacity < Dictionary.MAX_CAPACITY) {
                if (dictionary.Size > dictionary.Capacity * Dictionary.MAX_SIZE) {
                    throw new TestException("dictionary.Size is too big. Capacity: " + dictionary.Capacity + " fullness: " + dictionary.Size / (float) dictionary.Capacity);
                }
            }
            if (dictionary.Capacity <= Dictionary.MAX_CAPACITY) {
                if (dictionary.SearchSize > dictionary.Capacity * Dictionary.MAX_SEARCH_SIZE) {
                    throw new TestException("dictionary.SearchSize is too big. Capacity: " + dictionary.Capacity + " fullness: " + dictionary.SearchSize / (float) dictionary.Capacity);
                }
            }
            if (dictionary.Capacity > Dictionary.MIN_CAPACITY) {
                if (dictionary.Size < dictionary.Capacity * Dictionary.MIN_SIZE) {
                    throw new TestException("dictionary.Size is too small. Capacity: " + dictionary.Capacity + " fullness: " + dictionary.Size / (float) dictionary.Capacity);
                }
            }

            if (dictionary.Size > dictionary.SearchSize) {
                throw new TestException("dictionary.Size: " + dictionary.Size + " > dictionary.SearchSize: " + dictionary.SearchSize);
            }

            uint capacity = dictionary.Capacity;
            while (capacity > 1) {
                if ((capacity & 1) == 1) {
                    throw new TestException("dictionary.Capacity is not power of 2: " + dictionary.Capacity);
                }
                capacity >>= 1;
            }

            if ((dictionary.HASH_CONST_A & 1) == 0) {
                throw new TestException("dictionary.HASH_CONST_A is even: " + dictionary.HASH_CONST_A);
            }
            if ((dictionary.HASH_CONST_B & 1) == 0) {
                throw new TestException("dictionary.HASH_CONST_B is even: " + dictionary.HASH_CONST_B);
            }

            if (dictionary.Keys.Length != dictionary.Capacity) {
                throw new TestException("dictionary.Keys.Length: " + dictionary.Keys.Length + " != dictionary.Capacity: " + dictionary.Capacity);
            }
            if (dictionary.Values.Length != dictionary.Capacity) {
                throw new TestException("dictionary.Values.Length: " + dictionary.Values.Length + " != dictionary.Capacity: " + dictionary.Capacity);
            }
            if (dictionary.IsUsed.Length != dictionary.Capacity) {
                throw new TestException("dictionary.IsUsed.Length: " + dictionary.IsUsed.Length + " != dictionary.Capacity: " + dictionary.Capacity);
            }
            if (dictionary.WasUsed.Length != dictionary.Capacity) {
                throw new TestException("dictionary.WasUsed.Length: " + dictionary.WasUsed.Length + " != dictionary.Capacity: " + dictionary.Capacity);
            }

            uint realSearchSize = 0;
            uint realSize = 0;
            for (uint i = 0; i < dictionary.Capacity; i++) {
                if (dictionary.IsUsed[i]) {
                    realSize ++;
                }
                if (dictionary.WasUsed[i]) {
                    realSearchSize ++;
                }
            }
            if (dictionary.Size != realSize) {
                throw new TestException("incorrect dictionary.Size: " + dictionary.Size + ", correct: " + realSize);
            }
            if (dictionary.SearchSize != realSearchSize) {
                throw new TestException("incorrect dictionary.SearchSize: " + dictionary.SearchSize + ", correct: " + realSearchSize);
            }

            for (uint i = 0; i < dictionary.Capacity; i++) {
                if (dictionary.IsUsed[i] && ! dictionary.WasUsed[i]) {
                    throw new TestException("dictionary.IsUsed[" + i + "] && ! dictionary.WasUsed[" + i + "]");
                }
            }

            HashSet<int> set = new HashSet<int>();
            for (uint i = 0; i < dictionary.Capacity; i++) {
                if (dictionary.IsUsed[i]) {
                    if (set.Contains(dictionary.Keys[i])) {
                        throw new TestException("duplicate dictionary key: " + dictionary.Keys[i]);
                    }
                    set.Add(dictionary.Keys[i]);
                }
            }

            if (dictionary.Size != wrapper.Test.Count()) {
                throw new TestException("dictionary.Size is different, than test dictionary size");
            }

            int?[] values = wrapper.Get(KeysInterval, false);
            int?[] testValues = wrapper.Get(KeysInterval, true);
            for (int i = 0; i < KeysInterval.Length; i++) {
                if (values[i] != testValues[i]) {
                    throw new TestException("same key: " + KeysInterval[i] + " has different values in dictionary: " + values[i] + " and test dictionary: " + testValues);
                }
            }
        }

        // Test all wrappers, which are being watched.
        public void Test() {
            foreach (TestWrapper wrapper in Wrappers) {
                TestDictionary(wrapper);
            }
        }

        // Print test specifications, if it is first test. (Tests repeat each test x1000)
        public void Start(string message, int repetitionNumber) {
            if (repetitionNumber == 0) {
                Console.Write("    " + message.PadRight(45));
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