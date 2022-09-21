using System;

namespace DataStructures {
    // You can add key:value pair (both int).
    // Only one pair can be saved in each cell, hashing must be repeated multiple times.
    class Dictionary {
        private static Random random = new Random();

        public static uint MIN_CAPACITY = 1 << 3;
        public static uint MAX_CAPACITY = (uint) 1 << 31;

        public static float MAX_SEARCH_SIZE = 0.85f;
        public static float MAX_SIZE = 0.7f;
        public static float MIN_SIZE = 0.25f;

        // f(x, i) := ( A*x + B*x*i ) % Capacity;
        // if B*x would be even, it is increased by 1
        public uint HASH_CONST_A;
        public uint HASH_CONST_B;

        public uint Capacity; // number of cells
        public uint SearchSize; // number of true values in WasUsed
        public uint Size; // number of true values in IsUsed

        public int[] Keys;
        public int[] Values;
        public bool[] IsUsed;
        public bool[] WasUsed;

        public Dictionary() {
            Capacity = MIN_CAPACITY;
            SearchSize = 0;
            Size = 0;

            Keys = new int[Capacity];
            Values = new int[Capacity];
            IsUsed = new bool[Capacity];
            WasUsed = new bool[Capacity];

            HASH_CONST_A = CreateHashConstant();
            HASH_CONST_B = CreateHashConstant();
        }

        // Create random odd uint value
        private uint CreateHashConstant() {
            return (uint) random.Next(1, 1 << 30) * 2 + 1;
        }

        // Removes all saved values, then adds them again
        // It is possible to change Capacity
        private void Rehash(uint capacity) {
            uint oldCapacity = Capacity;

            Capacity = capacity;
            SearchSize = 0;
            Size = 0;
            
            int[] keys = Keys;
            int[] values = Values;
            bool[] isUsed = IsUsed;

            Keys = new int[Capacity];
            Values = new int[Capacity];
            IsUsed = new bool[Capacity];
            WasUsed = new bool[Capacity];

            for (uint i = 0; i < oldCapacity; i++) {
                if (isUsed[i]) {
                    Add(keys[i], values[i]);
                }
            }
        }

        // First part of hash function (A*x)
        private uint HashA(uint key) {
            return HASH_CONST_A * key;
        }

        // Second part of hash function (B*x), but is increased by 1, if it would be even
        private uint HashB(uint key) {
            uint output = HASH_CONST_B * key;
            if (output % 2 == 0) {
                output ++;
            }
            return output;
        }

        // Whole hash function f(x, i) := ( a(x) + b(x)*i ) % Capacity;
        private uint Hash(int key, uint i) {
            uint unsignedKey = (uint) (key - Int32.MinValue);
            return (HashA(unsignedKey) + HashB(unsignedKey) * i) % Capacity;
        }

        // Get position (hash) of key, where it is saved, if it exists.
        // Get first position (hash), where there is and was not saved anything, if the key does not exist (WasSaved[h] = false)
        private uint? GetPosition(int key) {
            uint i = 0;
            uint hash = Hash(key, i);
            while (WasUsed[hash] && Keys[hash] != key) {
                i ++;
                hash = Hash(key, i);
            }

            return IsUsed[hash] ? hash : null;
        }

        // Add key:value pair, if the key does not exist.
        // Change value, if the key does exist.
        public void Add(int key, int value) {
            uint? knownHash = GetPosition(key);

            uint hash;
            if (knownHash == null) {
                uint i = 0;
                hash = Hash(key, i);
                while (IsUsed[hash]) {
                    i ++;
                    hash = Hash(key, i);
                }
            } else {
                hash = (uint) knownHash;
            }

            if (! IsUsed[hash]) {
                IsUsed[hash] = true;
                Size ++;
            }
            if (! WasUsed[hash]) {
                WasUsed[hash] = true;
                SearchSize ++;
            }

            Keys[hash] = key;
            Values[hash] = value;

            if (Size > Capacity * MAX_SIZE && Capacity < MAX_CAPACITY) {
                Rehash(Capacity * 2);
            } else if (SearchSize > Capacity * MAX_SEARCH_SIZE && SearchSize > Size * 1.1f) {
                Rehash(Capacity);
            }
        }

        // @return true, if key exists.
        public bool Has(int key) {
            uint i = 0;
            uint hash = Hash(key, i);
            while (WasUsed[hash] && Keys[hash] != key) {
                i ++;
                hash = Hash(key, i);
            }

            return IsUsed[hash];
        }

        // @return value, if key does exist
        // @return null, if key does not exist
        public int? Get(int key) {
            uint i = 0;
            uint hash = Hash(key, i);
            while (WasUsed[hash] && Keys[hash] != key) {
                i ++;
                hash = Hash(key, i);
            }

            if (! IsUsed[hash]) {
                return null;
            }

            return Values[hash];
        }

        // Remove key:value pair, if it exists
        // @return value if key existed
        // @return null if key did not exist
        public int? Remove(int key) {
            uint i = 0;
            uint hash = Hash(key, i);
            while (WasUsed[hash] && Keys[hash] != key) {
                i ++;
                hash = Hash(key, i);
            }

            if (! IsUsed[hash]) {
                return null;
            }

            IsUsed[hash] = false;
            Size --;

            int value = Values[hash];

            if (Size < Capacity * MIN_SIZE && Capacity > MIN_CAPACITY) {
                Rehash(Capacity / 2);
            }

            return value;
        }

        // Print key:value, where there (IsUsed[h] && WasUsed[h])
        // Print + where there (! WasUsed[h] && IsUsed[h])
        // Print - where there (! IsUsed[h])
        public void Print() {
            Console.Write(Size + "/" + SearchSize + "/" + Capacity + " [ ");
            for (int i = 0; i < Capacity; i++) {
                if (! WasUsed[i]) {
                    Console.Write("- ");
                } else if (! IsUsed[i]) {
                    Console.Write("+ ");
                } else {
                    Console.Write(Keys[i] + ":" + Values[i] + " ");
                }
            }
            Console.WriteLine("]");
        }
    }
}