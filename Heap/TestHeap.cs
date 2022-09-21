using System;

using System.Collections.Generic;

using TestException = Test.TestException;
using DataStructuresException = DataStructures.DataStructuresException;

namespace TestHeap
{
    // Fake Heap structure. Is used by automatic testing as comparement.
    class TestHeap
    {
        public List<int> Keys;
        public int Size;

        public TestHeap() {
            Keys = new List<int>();
            Size = Keys.Count();
        }

        public TestHeap(TestHeap heap1, TestHeap heap2) {
            Keys = new List<int>();
            foreach (int key in heap1.Keys) {
                Keys.Add(key);
            }
            foreach (int key in heap2.Keys) {
                Keys.Add(key);
            }
            Size = Keys.Count();
        }

        public void Add(int key) {
            Keys.Add(key);
            Size = Keys.Count();
        }

        public void Remove(int key) {
            for (int i = 0; i < Size; i++) {
                if (Keys[i] == key) {
                    Keys.RemoveAt(i);
                    Size = Keys.Count();
                    return;
                }
            }

            throw new TestException("TestHeap can not remove non-existent key (" + key + ").");
        }

        public int GetMin() {
            if (Size == 0) {
                throw new TestException("TestHeap can not return minimal key, because it is empty.");
            }

            int minKey = Keys[0];
            for (int i = 0; i < Size; i++) {
                if (Keys[i] < minKey) {
                    minKey = Keys[i];
                }
            }

            return minKey;
        }

        public int RemoveMin() {
            if (Size == 0) {
                throw new TestException("TestHeap can not remove minimal key, because it is empty.");
            }

            int minPosition = 0;
            int minKey = Keys[0];

            for (int i = 0; i < Size; i++) {
                if (Keys[i] < minKey) {
                    minKey = Keys[i];
                    minPosition = i;
                }
            }

            Keys.RemoveAt(minPosition);
            Size = Keys.Count();

            return minKey;
        }

        public void Change(int previousKey, int key) {
            for (int i = 0; i < Size; i++) {
                if (Keys[i] == previousKey) {
                    Keys[i] = key;
                    return;
                }
            }

            throw new TestException("TestHeap can not change non-existent key (" + previousKey + " -> " + key + ").");
        }

        public void Union(TestHeap heap) {
            foreach (int key in heap.Keys) {
                Add(key);
            }
        }

        public void Print() {
            Console.Write("< ");
            foreach (int key in Keys) {
                Console.Write(key + " ");
            }
            Console.WriteLine(">");
        }
    }
}