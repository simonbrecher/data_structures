using System;

namespace DataStructures
{
    // One "vertex" for heap
    // Can be used to change Data
    // Can be used to specify "vertex" to change or remove key by heap
    class HeapVertex
    {
        public int Key; // Index in Heap.Vertices (first vertex has Position = 0)
        public int Position;
        // true: Vertex is still in Heap.
        // false: Vertex was removed from Heap.
        public bool IsAlive;

        // User can add any value to a vertex.
        // Default value: null
        public int? Data;

        public HeapVertex(int key, int position, int? data = null) {
            Key = key;
            Position = position;
            IsAlive = true;
            Data = data;
        }

        public HeapVertex Deepcopy() {
            return new HeapVertex(Key, Position, Data);
        }
    }
}