using System;

using BinaryTree = DataStructures.BinaryTree;

namespace TestBinaryTree
{
    // Joins BinaryTree and TestBinaryTree. Calls same methods on both of them.
    class TestWrapper
    {
        public BinaryTree Tree;
        public TestBinaryTree Test;

        public TestWrapper() {
            Tree = new BinaryTree();
            Test = new TestBinaryTree();
        }

        public void Add(int key) {
            Tree.Add(key);
            Test.Add(key);
        }

        public void Remove(int key) {
            Tree.Remove(key);
            Test.Remove(key);
        }

        public bool Has(int key, bool isTest) {
            return isTest ? Test.Has(key) : Tree.Has(key);
        }

        public bool[] Has(int[] keys, bool isTest) {
            if (isTest) {
                return Test.Has(keys);
            } else {
                bool[] output = new bool[] {};
                foreach(int key in keys) {
                    output.Append(Tree.Has(key));
                }
                return output;
            }
        }

        public int[] Enumerate(bool isTest) {
            return isTest ? Test.Enumerate() : Tree.Enumerate();
        }

        public TestWrapper Deepcopy() {
            TestWrapper wrapper = new TestWrapper();
            wrapper.Tree = Tree.Deepcopy();
            wrapper.Test = Test.Deepcopy();
            return wrapper;
        }
    }
}