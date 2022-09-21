using System;

using System.Collections.Generic;

using BinaryTree = DataStructures.BinaryTree;
using TestException = Test.TestException;

namespace TestBinaryTree
{
    // This class checks, if BinaryTree is in correct format and if the outputs are same as in TestBinaryTree.
    // You can watch more than one TestWrapper.
    class Tester
    {
        private List<TestWrapper> Wrappers;
        public int[] Has = new int[] {};

        // @parameter specify minimal and maximal value of key.
        public Tester(int hasMin, int hasMax) {
            Wrappers = new List<TestWrapper>();
            SetHasInterval(hasMin, hasMax);
        }

        // Create array with all possible keys. Used in constructor
        private void SetHasInterval(int hasMin, int hasMax) {
            Has = new int[] {};
            for (int i = hasMin; i < hasMax; i++) {
                Has.Append(i);
            }
        }

        // If you want to include a wrapper in testing, add it here.
        public void Watch(TestWrapper wrapper) {
            Wrappers.Add(wrapper);
        }

        // Test vertex. (Recursive)
        private void TestVertex(BinaryTree tree) {
            if (tree.Size < 0) {
                throw new TestException("tree.Size < 0, tree.Size: " + tree.Size);
            }
            if (tree.Depth < -1) {
                throw new TestException("tree.Depth < -1, tree.Depth: " + tree.Depth);
            }
            if (tree.HasKey != (tree.Size != 0)) {
                throw new TestException("(tree.HasKey) != (tree.Size == 0), tree.Haskey: " + tree.HasKey + " , tree.Size: " + tree.Size);
            }

            int depth = Math.Max(tree.Left.Depth, tree.Right.Depth) + (tree.HasKey ? 1 : 0);
            int size = tree.Left.Size + tree.Right.Size + (tree.HasKey ? 1 : 0);

            if (depth != tree.Depth) {
                throw new TestException("Unexpected tree.Depth: " + tree.Depth + " , expected: " + depth);
            }
              if (size != tree.Size) {
                throw new TestException("Unexpected tree.Size: " + tree.Size + " , expected: " + size);
            }

            if (Math.Abs(tree.Left.Depth - tree.Right.Depth) > 1) {
                throw new TestException("Tree is not balanced. tree.Left.Depth: " + tree.Left.Depth + " , tree.Right.Depth: " + tree.Right.Depth);
            }

            if (tree.Left != BinaryTree.EMPTY) {
                if (tree.Left.Size == 0) {
                    throw new TestException("tree.Left.Size == 0, but tree.Left != BinaryTree.EMPTY");
                } else {
                    TestVertex(tree.Left);
                }
            }
            if (tree.Right != BinaryTree.EMPTY) {
                if (tree.Right.Size == 0) {
                    throw new TestException("tree.Right.Size == 0, but tree.Right != BinaryTree.EMPTY");
                } else {
                    TestVertex(tree.Right);
                }
            }
        }

        // Test whole tree. (Not recursive)
        private void TestTree(TestWrapper wrapper) {
            int[] enumerate = wrapper.Enumerate(false);
            int[] testEnumerate = wrapper.Enumerate(true);

            if (enumerate.Length != testEnumerate.Length) {
                throw new TestException("tree.enumerate() has wrong length: " + enumerate.Length + " , expected: " + testEnumerate.Length);
            }

            for (int i = 0; i < enumerate.Length; i++) {
                if (enumerate[i] != testEnumerate[i]) {
                    throw new TestException("Incorrect value in tree.enumerate()");
                }
            }

            bool[] has = wrapper.Has(Has, false);
            bool[] testHas = wrapper.Has(Has, true);

            for (int i = 0; i < has.Length; i++) {
                if (has[i] != testHas[i]) {
                    throw new TestException("Incorrect value in tree.has(): " + has[i] + " , expected: " + testHas[i]);
                }
            }
        }

        // Test all static values. (BinaryTree.EMPTY)
        private void TestStatic() {
            BinaryTree empty = BinaryTree.EMPTY;
            if (empty.Size != 0) {
                throw new TestException("BinaryTree.EMPTY.Size != 0");
            }
            if (empty.Depth != -1) {
                throw new TestException("BinaryTree.EMPTY.Depth != -1");
            }
            if (empty.HasKey) {
                throw new TestException("BinaryTree.EMPTY.HasKey is True");
            }
            if (empty.Right != empty) {
                throw new TestException("BinaryTree.EMPTY.Left != BinaryTree.EMPTY");
            }
            if (empty.Left != empty) {
                throw new TestException("BinaryTree.EMPTY.Right != BinaryTree.EMPTY");
            }
        }

        // Test static data and all wrappers, which are watched.
        public void Test() {
            TestStatic();
            foreach (TestWrapper wrapper in Wrappers) {
                TestTree(wrapper);
                TestVertex(wrapper.Tree);
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