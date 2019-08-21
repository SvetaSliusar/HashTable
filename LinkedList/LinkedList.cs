using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T> : IEnumerable<T>
    {
        LinkedListNode<T> _head;

        public int Length { get; private set; }

        public LinkedList()
        {
            _head = null;
            Length = 0;
        }

        public void Add(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            if (_head == null)
                _head = node;
            else
            {
                node.Next = _head;
                node.Previous = _head.Previous;
                _head.Previous = node;                
            }
            Length++;
        }

        public void AddAt(int index, T value)
        {
            LinkedListNode<T> nodeToAdd = new LinkedListNode<T>(value);
            if (index == 1)
                _head = nodeToAdd;

            int i = 1;
            LinkedListNode<T> currentNode = _head;

            while (index != i)
            {
                currentNode = currentNode.Next;
                i++;
            }

            nodeToAdd.Previous = currentNode.Previous;
            nodeToAdd.Next = currentNode;
            currentNode.Previous.Next = nodeToAdd;
            currentNode.Previous = nodeToAdd;

            Length++;
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = Find(value);
            if (node != null)
            {
                Remove(node);
                return true;
            }
            return false;
        }

        public void Remove(LinkedListNode<T> node)
        {
            var nodeToRemove = Find(node.Value);
            if (nodeToRemove == null)
                return;

            if (_head == node)
                _head = node.Next;

            node.Next.Previous = node.Previous;
            node.Previous.Next = node.Next;

            node.Next = null;
            node.Previous = null;
            Length--;
        }

        private LinkedListNode<T> Find(T value)
        {
            if (value == null)
                return null;
            LinkedListNode<T> currentNode = _head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (currentNode != null)
            {
                do
                {
                    if (comparer.Equals(currentNode.Value, value))
                        return currentNode;
                    currentNode = currentNode.Next;
                }
                while (currentNode != _head);
            }
            return null;
        }

        public bool RemoveAt(int index)
        {
            if (index < 1 || index > Length - 1)
                return false;
            int i = 0;
            LinkedListNode<T> nodeToRemove = _head;

            while (index != i)
            {
                nodeToRemove = nodeToRemove.Next;
                i++;
            }

            if (_head == nodeToRemove)
                _head = nodeToRemove.Next;

            nodeToRemove.Next.Previous = nodeToRemove.Previous;
            nodeToRemove.Previous.Next = nodeToRemove.Next;

            nodeToRemove.Next = null;
            nodeToRemove.Previous = null;
            Length--;

            return true;
        }

        public LinkedListNode<T> ElementAt(int index)
        {
            if (index == 1)
                return _head;
            if (index < 1 || index > Length - 1)
                return null;
            if (index == Length - 1)
                return _head.Previous;

            int i = 1;
            LinkedListNode<T> currentNode = _head;

            while (index != i)
            {
                currentNode = currentNode.Next;
                i++;
            }
            return currentNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator : IEnumerator<T>
        {
            LinkedList<T> _list;
            LinkedListNode<T> _node;
            int _index;
            T _current;

            internal Enumerator(LinkedList<T> list)
            {
                _list = list;
                _index = 0;
                _node = list._head;
                _current = default(T);
            }

            public T Current => (_index == 0 || _index > _list.Length) 
                ? throw new Exception() 
                : _current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                if (_node == null)
                {
                    return false;
                }

                _current = _node.Value;
                _node = _node.Next;
                if (_node == _list._head)
                {
                    _node = null;
                }
                _index++;
                return true;
            }

            public void Reset()
            {
                _node = _list._head;
                _current = default(T);
                _index = 0;
            }
        }
    }
}
