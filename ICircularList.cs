using System;
using System.Collections.Generic;

namespace DataStructures
{
    public interface ICircularList<T>
    {
        /// <summary>
        /// Adds a new element to the end of the list.
        /// </summary>
        /// <param name="element">The element which will be added to the linked list.</param>
        void Add(T element);

        /// <summary>
        /// Inserts a new element before the provided value.
        /// </summary>
        /// <param name="element">The elemnt which will be added to the list.</param>
        /// <param name="before">The new element will be inserted before this element.</param>
        bool InsertBefore(T element, T before);

        /// <summary>
        /// Inserts a new element after the provided value.
        /// </summary>
        /// <param name="element">The elemnt which will be added to the list</param>
        /// <param name="after">The new element will be inserted after this element.</param>
        bool InsertAfter(T element, T after);

        /// <summary>
        /// Inserts a new element to the provided position.
        /// </summary>
        /// <param name="element">The element which will be added to the list.</param>
        /// <param name="position">The index, where the new item will be.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the position is bigger than the
        /// length of the list it throws ArgumentOutOfRangeException.</exception>
        void Insert(T element, int position);

        /// <summary>
        /// Returns the index of the element in list.
        /// </summary>
        /// <param name="element">The index of the first occurence of this element will be returned.</param>
        /// <returns>The index of the element in list. If the element is not in the list -1 is returned.</returns>
        int IndexOf(T element);

        /// <summary>
        /// Returns the element, which matches the specified condition.
        /// </summary>
        /// <param name="action">The condition for what every item will be tested.</param>
        /// <exception cref="ItemNotFoundException">Throws exception if the item is not found in the list.</exception>
        /// <returns>Returns the first value that matches the condition. If nothing matches the condition then it returns null.</returns>
        T FindWhere(Func<T, bool> action);

        /// <summary>
        /// Returns a list containing the indexes of the occurences of the provided element.
        /// </summary>
        /// <param name="element">The elements positions will be return in the list.</param>
        /// <returns>A list containing the indexes of the occurences of the provided element</returns>
        ICircularList<int> Occurences(T element);

        /// <summary>
        /// Gets the item at the index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when list is empty.</exception>
        T Get(int index);

        /// <summary>
        /// Get the next value in the list. If its the last element it returns the first one.
        /// </summary>
        /// <returns>Return the next element in the list.</returns>
        T Next();

        /// <summary>
        /// Loops through the list once.
        /// </summary>
        /// <param name="action">Defines an action which will be executed with the current element.</param>
        void ForEach(Action<T> action);

        /// <summary>
        /// Removes the element at the provided index.
        /// </summary>
        /// <param name="index">The index of the element which will be removed.</param>
        T RemoveAt(int index);

        /// <summary>
        /// Removes the first occurence of the provided element.
        /// </summary>
        /// <param name="element">The element which will be removed.</param>
        /// <returns>False if element is not found in the list, otherwise returns true.</returns>
        bool Remove(T element);

        /// <summary>
        /// Clears the list.
        /// </summary>
        void Clear();

        /// <summary>
        /// Enumerator use for foreach method.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> GetEnumerator();
    }
}
