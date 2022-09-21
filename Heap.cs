using System;

namespace DataStructures
{
    // Binary minimum heap. Contains int keys (can be duplicate). Every key can have one int? variable (data) saved by user.
    // "vertices" can be changed and removed by its objects. "vertices" are saved in an array (first at index 0)
    class Heap
    {
        public List<HeapVertex> Vertices;
        public int Size;

        // Create an empty Heap.
        public Heap() {
            Vertices = new List<HeapVertex>();
            Size = 0;
        }

        // Create a Heap as a union of two Heaps.
        // Time complexity is O(N + M*log(M)), where N is size of bigger heap and M is size of smaller heap.
        public Heap(Heap heap1, Heap heap2) {
            Vertices = new List<HeapVertex>();
            Size = 0;

            Heap bigHeap = heap1.Size > heap2.Size ? heap1 : heap2;
            Heap smallHeap = heap1.Size > heap2.Size ? heap2 : heap1;

            SetToDeepcopy(bigHeap);
            foreach (HeapVertex vertex in smallHeap.Vertices) {
                Add(vertex.Key, vertex.Data);
            }
        }

        // Get position of parent vertex.
        private int GetParent(int position) {
            if (position == 0) {
                throw new DataStructuresException("Vertex with position 0 has no parent.");
            }
            return (position - 1) / 2;
        }

        // Get parent vertex.
        private HeapVertex GetParent(HeapVertex vertex) {
            return Vertices[GetParent(vertex.Position)];
        }

        // Get position of child vertex.
        // @param direction: 0 - left, 1 - right
        private int GetChild(int position, int direction) {
            return 2 * position + 1 + direction;
        }

        // Get child vertex.
        // @param direction: 0 - left, 1 - right
        private HeapVertex GetChild(HeapVertex vertex, int direction) {
            return Vertices[GetChild(vertex.Position, direction)];
        }

        // Switch position of two vertices.
        private void Switch(int position1, int position2) {
            HeapVertex vertex1 = Vertices[position1];
            Vertices[position1] = Vertices[position2];
            Vertices[position1].Position = position1;
            Vertices[position2] = vertex1;
            Vertices[position2].Position = position2;
        }

        // Switch positions of vertex and its parent vertex.
        private void MoveUp(HeapVertex bottomVertex) {
            int bottomPosition = bottomVertex.Position;
            int topPosition = GetParent(bottomPosition);

            Switch(bottomPosition, topPosition);
        }

        // When key of a vertex is changed, repair Heap.
        private void Repair(HeapVertex vertex) {
            bool changed = true;
            while (changed) {
                changed = false;
                if (vertex.Position != 0 && vertex.Key < GetParent(vertex).Key) {
                    MoveUp(vertex);
                    changed = true;
                } else {
                    bool hasLeft = GetChild(vertex.Position, 0) < Size;
                    bool hasRight = GetChild(vertex.Position, 1) < Size;
                    if (hasLeft && hasRight) {
                        HeapVertex left = GetChild(vertex, 0);
                        HeapVertex right = GetChild(vertex, 1);
                        if (left.Key <= right.Key) {
                            if (vertex.Key > left.Key) {
                                MoveUp(left);
                                changed = true;
                            }
                        } else {
                            if (vertex.Key > right.Key) {
                                MoveUp(right);
                                changed = true;
                            }
                        }
                    } else if (hasLeft) {
                        HeapVertex left = GetChild(vertex, 0);
                        if (vertex.Key > left.Key) {
                            MoveUp(left);
                            changed = true;
                        }
                    }
                }
            }
        }

        // Add new heap vertex.
        // @return object of removed vertex
        public HeapVertex Add(int key, int? data = null) {
            HeapVertex vertex = new HeapVertex(key, Size, data);
            Vertices.Add(vertex);
            Repair(vertex);
            Size = Vertices.Count;
            return vertex;
        }

        // Check if the vertex belongs to the Heap.
        private bool checkVertex(HeapVertex vertex) {
            if (vertex.Position >= Size) {
                return false;
            } else if (vertex != Vertices[vertex.Position]) {
                return false;
            }

            return true;
        }

        // Remove vertex by its object.
        public void Remove(HeapVertex vertex) {
            if (! checkVertex(vertex)) {
                throw new DataStructuresException("The Heap can not remove this vertex, because the vertex does not belong to the Heap.");
            }

            HeapVertex lastVertex = Vertices[Size - 1];
            Switch(vertex.Position, lastVertex.Position);
            Vertices[Size - 1].IsAlive = false;
            Vertices.RemoveAt(Size - 1);
            Size = Vertices.Count;
            Repair(lastVertex);
        }

        // Get vertex with lowest key.
        public HeapVertex GetMin() {
            if (Size == 0) {
                throw new DataStructuresException("Heap can not return minimal vertex, because it is empty.");
            }
            
            return Vertices[0];
        }

        // Get vertex with lowest key and remove it from heap.
        public HeapVertex RemoveMin() {
            HeapVertex vertex = Vertices[0];
            Remove(vertex);
            return vertex;
        }

        // Change key value of a vertex.
        public void Change(HeapVertex vertex, int key) {
            if (! checkVertex(vertex)) {
                throw new DataStructuresException("The Heap can not change this vertex, because the vertex does not belong to the Heap.");
            }

            vertex.Key = key;
            Repair(vertex);
        }

        // Add all keys and values from another Heap.
        public void Union(Heap heap) {
            foreach (HeapVertex vertex in heap.Vertices) {
                Add(vertex.Key, vertex.Data);
            }
        }

        // Set this Heap to be a deepcopy of another Heap.
        private void SetToDeepcopy(Heap heap) {
            foreach (HeapVertex vertex in Vertices) {
                vertex.IsAlive = false;
            }
            foreach (HeapVertex vertex in heap.Vertices) {
                Vertices.Add(vertex.Deepcopy());
            }
            Size = heap.Size;
        }

        // Get a new Heap with same vertices. (using deepcopy)
        public Heap Deepcopy() {
            Heap copy = new Heap();
            foreach (HeapVertex vertex in Vertices) {
                copy.Vertices.Add(vertex.Deepcopy());
            }
            copy.Size = Size;
            return copy;
        }

        // @return array with all keys in order, in which they are saved
        public int[] EnumerateKeys() {
            int[] keys = new int[Size];
            for (int i = 0; i < Size; i++) {
                keys[i] = Vertices[i].Key;
            }
            return keys;
        }

        // @return array with all data in order, in which they are saved
        public int?[] EnumerateData() {
            int?[] data = new int?[Size];
            for (int i = 0; i < Size; i++) {
                data[i] = Vertices[i].Data;
            }
            return data;
        }

        public void Print() {
            Console.Write(Size + " [ ");
            foreach (HeapVertex vertex in Vertices) {
                Console.Write("(" + vertex.Position + " " + vertex.Key + " " + vertex.Data + " " + (vertex.IsAlive ? "T" : "F") + ") ");
            }
            Console.WriteLine("]");
        }

        public void PrintKeys() {
            int[] keys = EnumerateKeys();
            Console.Write("[ ");
            foreach (int key in keys) {
                Console.Write(key + " ");
            }
            Console.WriteLine("]");
        }

        public void PrintData() {
            int?[] datas = EnumerateData();
            Console.Write("[ ");
            foreach (int? data in datas) {
                Console.Write(data + " ");
            }
            Console.WriteLine("]");
        }
    }
}