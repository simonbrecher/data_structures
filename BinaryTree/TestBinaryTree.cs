using System;

namespace TestBinaryTree
{
    // Fake BinaryTree structure. Is used by automatic testing as comparement.
    class TestBinaryTree
    {
        public List<int> Values;
        public int Size;

        public TestBinaryTree() {
            Values = new List<int>();
            Size = Values.Count;
        }

        public void Add(int key) {
            int position = 0;
            while (position < Values.Count && Values[position] < key) {
                position ++;
            }
            if (position == Values.Count || Values[position] != key) {
                Values.Insert(position, key);
            }
        }

        public void Remove(int key) {
            Values.Remove(key);
        }

        public bool Has(int key) {
            return Values.Contains(key);
        }

        public bool[] Has(int[] keys) {
            bool[] output = new bool[keys.Length];
            int position = 0;
            for (int i = 0; i < keys.Length; i++) {
                while (Values[position] < keys[i]) {
                    position ++;
                }
                output[i] = Values[position] == keys[i];
            }
            return output;
        }

        public int[] Enumerate() {
            return Values.ToArray();
        }

        public TestBinaryTree Deepcopy() {
            TestBinaryTree tree = new TestBinaryTree();
            for (int i = 0; i < Values.Count; i++) {
                tree.Values.Append(Values[i]);
            }
            tree.Size = Size;
            return tree;
        }
    }
}