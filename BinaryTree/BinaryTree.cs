using System;

namespace DataStructures
{
    // AVL tree. Should be used as a set. Can contain only int.
    class BinaryTree
    {
        public static BinaryTree EMPTY;
        
        public bool HasKey; // false:   only for BinaryTree.EMPTY   or   root vertex of empty tree
        public int Key;
        public int Depth; // -1: empty tree ; X: longest path from root
        public int Size; // number of vertices
        public BinaryTree Left = EMPTY;
        public BinaryTree Right = EMPTY;

        // Create static representation of empty tree. (Used for unexisting children.)
        static BinaryTree() {
            EMPTY = new BinaryTree();
            EMPTY.Left = EMPTY;
            EMPTY.Right = EMPTY;
        }

        public BinaryTree() {
            Depth = -1;
            Size = 0;
            HasKey = false;
            Left = EMPTY;
            Right = EMPTY;
        }

        // Create new instance as shallow copy 
        private BinaryTree(BinaryTree tree) {
            Shallowcopy(tree);
        }

        // Update values Size and Depth
        private void Update() {
            if (HasKey) {
                Depth = Math.Max(Left.Depth, Right.Depth) + 1;
                Size = Left.Size + Right.Size + 1;
            } else { // for empty root OR BinaryTree.EMPTY
                Depth = -1;
                Size = 0;
            }
        }

        // Balance tree (Check if Left.Depth and Right.Depth are not different by more than 1)
        private void Balance() {
            if (Left.Depth == Right.Depth + 2) {
                //      y            x
                //     / \          / \
                //    x   C   ->   A   y
                //   / \              / \
                //  A   B            B   C
                //
                // A.Depth == (h + 1) ; B.Depth == (h) or (h + 1) ; C.Depth == (h)
                if (Left.Right.Depth <= Left.Left.Depth) {
                    BinaryTree root = this; // original position of y
                    BinaryTree y = new BinaryTree(root); // original value of y (shallow copy)
                    BinaryTree x = y.Left;
                    BinaryTree a = x.Left;
                    BinaryTree b = x.Right;
                    BinaryTree c = y.Right;

                    root.Shallowcopy(x);
                    root.Left = a;
                    root.Right = y;
                    y.Left = b;
                    y.Right = c;

                    y.Update();
                    root.Update();
                //      z               y
                //     / \             / \
                //    x   D           x   z
                //   / \      ->     / \ / \
                //  A   y           A  B C  D
                //     / \
                //    B   C
                //
                // A.Depth == (h) ; B.Depth == (h - 1) or (h) ; C.Depth == (h - 1) or (h) ; D.Depth == (h)
                } else {
                    BinaryTree root = this; // original position of z
                    BinaryTree z = new BinaryTree(root); // original value of z (shallow copy)
                    BinaryTree x = z.Left;
                    BinaryTree y = x.Right;
                    BinaryTree a = x.Left;
                    BinaryTree b = y.Left;
                    BinaryTree c = y.Right;
                    BinaryTree d = z.Right;

                    root.Shallowcopy(y);
                    root.Left = x;
                    root.Right = z;
                    x.Left = a;
                    x.Right = b;
                    z.Left = c;
                    z.Right = d;

                    x.Update();
                    z.Update();
                    root.Update();
                }
            } else if (Right.Depth == Left.Depth + 2) {
                //    x                y
                //   / \              / \
                //  A   y     ->     x   C
                //     / \          / \
                //    B   C        A   B
                //
                // A.Depth == (h) ; B.Depth == (h) or (h + 1) ; C.Depth == (h + 1)
                if (Right.Left.Depth <= Right.Right.Depth) {
                    BinaryTree root = this; // original position of x
                    BinaryTree x = new BinaryTree(root); // original value of x (shallow copy)
                    BinaryTree y = x.Right;
                    BinaryTree a = x.Left;
                    BinaryTree b = y.Left;
                    BinaryTree c = y.Right;

                    root.Shallowcopy(y);
                    root.Left = x;
                    root.Right = c;
                    x.Left = a;
                    x.Right = b;

                    x.Update();
                    root.Update();
                //    x                y
                //   / \              / \
                //  A   z            x   z
                //     / \   ->     / \ / \
                //    y   D        A  B C  D
                //   / \
                //  B   C
                //
                // A.Depth == (h) ; B.Depth == (h - 1) or (h) ; C.Depth == (h - 1) or (h) ; D.Depth == (h)
                } else {
                    BinaryTree root = this; // original position of x
                    BinaryTree x = new BinaryTree(root); // original value of x (shallow copy)
                    BinaryTree z = x.Right;
                    BinaryTree y = z.Left;
                    BinaryTree a = x.Left;
                    BinaryTree b = y.Left;
                    BinaryTree c = y.Right;
                    BinaryTree d = z.Right;
                    
                    root.Shallowcopy(y);
                    root.Left = x;
                    root.Right = z;
                    x.Left = a;
                    x.Right = b;
                    z.Left = c;
                    z.Right = d;

                    x.Update();
                    z.Update();
                    root.Update();
                }
            } else {
                Update();
            }
        }

        // Add key (if it does not exist)
        public void Add(int key) {
            if (Size == 0) {
                HasKey = true;
                Key = key;
            } else if (key < Key) {
                if (Left.Size == 0) {
                    Left = new BinaryTree();
                }
                Left.Add(key);
            } else if (key > Key) {
                if (Right.Size == 0) {
                    Right = new BinaryTree();
                }
                Right.Add(key);
            }
            Balance();
        }

        // When you want to remove a vertex with 2 children, you go once left and then go right as long as possible.
        // The vertex that you find will only have one child. You steal its key and remove this vertex instead.
        // This function goes right as long as possible, remove the last vertex and returns its key.
        // @return key value of removed vertice
        private int RemoveRight() {
            if (Right.Size == 0) {
                int key = Key;
                Shallowcopy(Left);
                Balance();
                return key;
            } else {
                int key = Right.RemoveRight();
                if (Right.Size == 0) {
                    Right = EMPTY;
                }
                Balance();
                return key;
            }
        }

        // Remove key (if it exists)
        public void Remove(int key) {
            if (Key == key) { // remove itself
                if (Left.Size == 0) {
                    Shallowcopy(Right);
                    Balance();
                } else if (Right.Size == 0) {
                    Shallowcopy(Left);
                    Balance();
                } else {
                    int key2 = Left.RemoveRight(); // go left once, then go right as long as possible. Remove the vertex at the end and steal its key.
                    Key = key2;
                    Balance();
                }
            } else { // look if we can remove 
                if (key < Key && Left.Size > 0) {
                    Left.Remove(key);
                    if (Left.Size == 0) {
                        Left = EMPTY;
                    }
                    Balance();
                } else if (key > Key && Right.Size > 0) {
                    Right.Remove(key);
                    if (Right.Size == 0) {
                        Right = EMPTY;
                    }
                    Balance();
                }
            }
            Update();
        }

        // @return true: if key exists
        public bool Has(int key) {
            if (Size == 0) {
                return false;
            } else if (key == Key) {
                return true;
            } else if (key < Key) {
                return Left.Has(key);
            } else {
                return Right.Has(key);
            }
        }

        // @return deepcopy of itself
        public BinaryTree Deepcopy() {
            BinaryTree tree = new BinaryTree(this); // shallow copy

            if (Left.Size != 0) {
                tree.Left = Left.Deepcopy();
            }
            if (Right.Size != 0) {
                tree.Right = Right.Deepcopy();
            }
            return tree;
        }

        // set itself to shallowcopy of another tree
        private void Shallowcopy(BinaryTree tree) {
            HasKey = tree.HasKey;
            Key = tree.Key;
            Left = tree.Left;
            Right = tree.Right;
            Depth = tree.Depth;
            Size = tree.Size;
        }

        // return key values in ascending order
        public int[] Enumerate() {
            int[] array = new int[Size];
            Enumerate(array, 0);
            return array;
        }

        // put key values in ascending order in an array with specified offset (used for recursion)
        private void Enumerate(int[] array, int offset) {
            if (Left.Size != 0) {
                Left.Enumerate(array, offset);
                offset += Left.Size;
            }
            if (Size > 0) {
                array[offset] = Key;
            }
            if (Right.Size != 0) {
                Right.Enumerate(array, offset + 1);
            }
        }

        // print tree structure in format, which can be used for manual checking (but only if tree is small enough)
        public void Print(int depth = 0, string side = "") {
            if (depth == 0) {
                Console.Write("[ ");
            }
            Console.Write(side + depth + " ");
            Console.Write(Size > 0 ? ("<" + Key + ">") : "- ");
            if (Size > 0 || Right.Size > 0) {
                Console.Write(" ");
            }
            if (Left.Size > 0) {
                Left.Print(depth + 1, "l");
            }
            if (Right.Size > 0) {
                Right.Print(depth + 1, "r");
            }
            if (depth == 0) {
                Console.Write("]");
                Console.WriteLine("");
            } else {
                Console.Write(".");
            }
        }

        // print key values in ascending order
        public void PrintFlat() {
            int[] array = Enumerate();

            Console.Write("[ ");
            foreach (int number in array) {
                Console.Write(number + " ");
            }
            Console.WriteLine("]");
        }
    }
}