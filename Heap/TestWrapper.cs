using System;

using Heap = DataStructures.Heap;
using HeapVertex = DataStructures.HeapVertex;
using TestException = Test.TestException;

namespace TestHeap
{
    // Joins Heap and TestHeap. Calls same methods on both of them.
    class TestWrapper
    {
        public Heap Heap;
        public TestHeap Test;

        public TestWrapper() {
            Heap = new Heap();
            Test = new TestHeap();
        }

        public TestWrapper(TestWrapper wrapper1, TestWrapper wrapper2) {
            Heap = new Heap();
            Test = new TestHeap();

            Union(wrapper1);
            Union(wrapper2);
        }

        public int GetMin(bool isTest) {
            return isTest ? Test.GetMin() : Heap.GetMin().Key;
        }

        public HeapVertex Add(int key, int? data) {
            HeapVertex vertex = Heap.Add(key, data);
            Test.Add(key);

            if (vertex.Key != key || vertex.Data != data) {
                throw new TestException("Heap.Add() returns vertex with incorrect key or data.");
            }

            return vertex;
        }

        public void Remove(HeapVertex vertex) {
            Heap.Remove(vertex);
            Test.Remove(vertex.Key);

            if (vertex.IsAlive) {
                throw new TestException("Heap.Remove() does not change HeapVertice.IsAlive to false.");
            }
        }

        public void RemoveMin() {
            HeapVertex vertex = Heap.RemoveMin();
            int key = Test.RemoveMin();

            if (vertex.Key != key) {
                throw new TestException("Heap.RemoveMin() returns vertex with different key than TestHeap.RemoveMin().");
            }

            if (vertex.IsAlive) {
                throw new TestException("Heap.RemoveMin() does not change HeapVertice.IsAlive to false.");
            }
        }

        public void Change(HeapVertex vertex, int key) {
            int previousKey = vertex.Key;

            Heap.Change(vertex, key);
            Test.Change(previousKey, key);
        }

        public void Union(TestWrapper wrapper) {
            Heap.Union(wrapper.Heap);
            Test.Union(wrapper.Test);
        }
    }
}