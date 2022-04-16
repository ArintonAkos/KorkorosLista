using KorkorosLista;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// Creates a cirular linked list object.
    /// </summary>
    /// <typeparam name="T">The type of the data stored.</typeparam>
    public class CircularList<T> : ICircularList<T>
    {
        #region Node Definition
        
        private class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }
        
        #endregion

        #region Private Methods

        private int GetPosition(int index)
        { 
            if (_Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index >= 0)
            {
                return index % Length;
            }

            return Length - (Math.Abs(index) % Length);
        }
        
        private bool IsEqual(T firstElement, T secondElement)
        {
            return EqualityComparer<T>.Default.Equals(firstElement, secondElement);
        }

        private Node TraverseUntil(int count)
        {
            if (Length == 0)
            {
                return null;
            }

            Node node = _Start;
            count = GetPosition(count);

            while (count > 0)
            {
                node = node.Next;
                count--;
            }

            return node;
        }

        #endregion

        #region Private Members

        private int _Length;
        private Node _Start = null;
        private Node _Selected = null;

        #endregion

        #region Constructors

        public CircularList(CircularList<T> items)
        {
            items.ForEach((item) => Add(item));
        }

        public CircularList(T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public CircularList()
        {
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void Add(T element)
        {
            Insert(element, _Length);
        }

        /// <inheritdoc />
        public bool InsertBefore(T element, T before)
        {
            int indexOfSearchedElement = IndexOf(before);

            if (indexOfSearchedElement == -1)
            {
                return false;
            }

            Insert(element, indexOfSearchedElement);

            return true;
        }

        /// <inheritdoc />
        public bool InsertAfter(T element, T after)
        {
            int indexOfSearchedElement = IndexOf(after);

            if (indexOfSearchedElement == -1)
            {
                return false;
            }

            Insert(element, indexOfSearchedElement + 1);

            return true;
        }

        /// <inheritdoc />
        public void Insert(T element, int position)
        {
            if (position > _Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Length == 0)
            {
                Node newNode = new Node
                {
                    Item = element,
                };
                newNode.Next = newNode;

                _Selected = newNode;
                _Start = newNode;
            }
            else 
            {
                Node elementBefore = TraverseUntil(position - 1);
                Node newNode = new Node
                {
                    Item = element,
                };

                newNode.Next = elementBefore.Next;
                elementBefore.Next = newNode;

                if (position == 0)
                {
                    if (_Selected == _Start)
                    {
                        _Selected = newNode;
                    }

                    _Start = newNode;
                }
            }

            _Length++;
        }

        /// <inheritdoc />
        public int IndexOf(T element)
        {
            if (_Start == null)
            {
                return -1;
            }

            int depth = 0;
            Node node = _Start;

            while (depth != _Length && !IsEqual(element, node.Item))
            {
                node = node.Next;
                depth++;
            }

            if (depth == _Length)
            {
                return -1;
            }

            return depth;
        }

        /// <inheritdoc />
        public ICircularList<int> Occurences(T element)
        {
            int depth = 0;
            Node node = _Start;
            CircularList<int> occurences = new CircularList<int>();

            while (node != null && depth != Length)
            {
                if (node.Item.Equals(element))
                {
                    occurences.Add(depth);
                }

                node = node.Next;
                depth++;
            }

            return occurences;
        }

        /// <inheritdoc />
        public T Get(int index)
        {
            int n = GetPosition(index);
            int i = 0;
            Node node = _Start;

            while (i != n)
            {
                node = node.Next;
                i++;
            }

            return node.Item;
        }

        /// <inheritdoc />
        public T RemoveAt(int index)
        {
            if (_Length == 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (_Length == 1)
            {
                Node node = _Start;
                Clear();

                return node.Item;
            }
            else
            {
                int position = GetPosition(index);
                Node elementBefore = TraverseUntil(position - 1);
                Node element = elementBefore.Next;

                elementBefore.Next = element.Next;
                _Length--;

                if (element == _Selected)
                {
                    _Selected = element.Next;
                }
                if (element == _Start)
                {
                    _Start = element.Next;
                }

                return element.Item;
            }
        }

        /// <inheritdoc />
        public bool Remove(T element)
        {
            int elementIndex = IndexOf(element);

            if (elementIndex == -1)
            {
                return false;
            }

            RemoveAt(elementIndex);

            return true;
        }

        /// <inheritdoc />
        public void Clear()
        {
            _Start = null;
            _Selected = null;
            _Length = 0;
        }

        /// <inheritdoc />
        public T Next()
        {
            T value = _Selected.Item;
            _Selected = _Selected.Next;

            return value;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            Node node = _Start;
            int count = 0;

            while (count != _Length)
            {
                yield return node.Item;
                node = node.Next;
                count++;
            }
        }

        /// <inheritdoc />
        public void ForEach(Action<T> action)
        {
            int count = 0;
            Node node = _Start;

            while (count < _Length)
            {
                action(node.Item);
                node = node.Next;
                count++;
            }
        }

        public T FindWhere(Func<T, bool> action)
        {
            bool found = false;
            T retVal = default;

            ForEach((item) =>
            {
                if (action(item))
                {
                    found = true;
                    retVal = item;

                    return;
                }
            });

            if (found)
            {
                return retVal;
            }

            throw new ItemNotFoundException();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// The length of the circular list.
        /// </summary>
        public int Length
        {
            get { return _Length; }
        }
        
        /// <summary>
        /// Gets or sets the item at the position by using the [] symbols.
        /// </summary>
        /// <param name="index">The position of the element.</param>
        /// <returns>The element if the getter was called.</returns>
        public T this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Insert(value, index);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (_Length == 0);
            }
        }

        #endregion
    }
}
