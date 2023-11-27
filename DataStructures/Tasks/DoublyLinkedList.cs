using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node head;
        private Node tail;
        private int length;

        public int Length => length;

        public void Add(T e)
        {
            Node newNode = new Node { Value = e };

            if (tail == null)
            {
                head = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
            }

            tail = newNode;
            length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index == length)
            {
                Add(e);

                return;
            }

            Node newNode = new Node { Value = e };

            if (index == 0)
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            else
            {
                Node current = head;

                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                newNode.Previous = current;
                current.Next.Previous = newNode;
                current.Next = newNode;
            }
            length++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator(head);
        }

        public void Remove(T item)
        {
            Node current = head;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        tail = current.Previous;
                    }

                    length--;

                    break;
                }

                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                head = current.Next;
            }

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                tail = current.Previous;
            }

            length--;

            return current.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public T Value;

            public Node Next;

            public Node Previous;
        }

        private class DoublyLinkedListEnumerator : IEnumerator<T>
        {
            private Node current;
            private readonly Node head;

            public DoublyLinkedListEnumerator(Node head)
            {
                this.head = head;
                current = null;
            }

            public T Current => current.Value;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (current == null)
                {
                    current = head;
                }
                else
                {
                    current = current.Next;
                }

                return current != null;
            }

            public void Reset()
            {
                current = null;
            }
        }
    }
}
