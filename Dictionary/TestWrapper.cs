using System;

using Dictionary = DataStructures.Dictionary;

namespace TestDictionary
{
    // Joins Datastructures.Dictionary and Dictionary<int, int>. Calls same methods on both of them.
    class TestWrapper
    {
        public Dictionary Dictionary;
        public Dictionary<int, int> Test;

        public TestWrapper() {
            Dictionary = new Dictionary();
            Test = new Dictionary<int, int>();
        }

        public void Add(int key, int value) {
            Dictionary.Add(key, value);
            if (Test.ContainsKey(key)) {
                Test[key] = value;
            } else {
                Test.Add(key, value);
            }
        }

        public void Remove(int key) {
            Dictionary.Remove(key);
            Test.Remove(key);
        }

        public bool[] Has(int[] keys, bool isTest) {
            bool[] has = new bool[keys.Length];
            for (int i = 0; i < keys.Length; i++) {
                has[i] = isTest ? Test.ContainsKey(keys[i]) : Dictionary.Has(keys[i]);
            }
            return has;
        }

        public int?[] Get(int[] keys, bool isTest) {
            int?[] values = new int?[keys.Length];
            for (int i = 0; i < keys.Length; i++) {
                if (isTest) {
                    values[i] = Test.ContainsKey(keys[i]) ? Test[keys[i]] : null;
                } else {
                    values[i] = Dictionary.Get(keys[i]);
                }
            }
            return values;
        }

        public void Print() {
            Dictionary.Print();
            Console.Write("{ ");
            foreach (KeyValuePair<int, int> kvp in Test) {
                Console.Write("{0}: {1}, ", kvp.Key, kvp.Value);
            }
            Console.WriteLine("}");
        }
    }
}