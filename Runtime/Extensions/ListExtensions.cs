using System;
using System.Linq;
using System.Collections.Generic;

namespace Utilities.Runtime
{
    public static class ListExtensions
    {
        private static Random rng;
        
        /// <summary>
        /// Updates the contents of the list to match the provided collection of items.
        /// 
        /// This method clears all existing elements in the list and replaces them with
        /// the elements from the provided <paramref name="items"/> collection.
        /// It is useful for refreshing a list with a new set of data while maintaining
        /// the same list instance.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the list and items collection.</typeparam>
        /// <param name="list">The list to be refreshed with the new set of items. Must not be null.</param>
        /// <param name="items">The collection of items to replace the current contents of the list. Must not be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if either <paramref name="list"/> or <paramref name="items"/> is null.
        /// </exception>
        public static void RefreshWith<T>(this List<T> list, IEnumerable<T> items)
        {
            if (list == null) throw new ArgumentNullException(nameof(list), "The list cannot be null.");
            if (items == null) throw new ArgumentNullException(nameof(items), "The items collection cannot be null.");
    
            list.Clear();
            list.AddRange(items);
        }

        /// <summary>
        /// Determines whether a collection is null or has no elements
        /// without having to enumerate the entire collection to get a count.
        ///
        /// Uses LINQ's Any() method to determine if the collection is empty,
        /// so there is some GC overhead.
        /// </summary>
        /// <param name="list">List to evaluate</param>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || !list.Any();
        }

        /// <summary>
        /// Creates a new list that is a copy of the original list.
        /// </summary>
        /// <param name="list">The original list to be copied.</param>
        /// <returns>A new list that is a copy of the original list.</returns>
        public static List<T> Clone<T>(this IList<T> list)
        {
            return list.ToList();
        }

        /// <summary>
        /// Swaps two elements in the list at the specified indices.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of the first element.</param>
        /// <param name="indexB">The index of the second element.</param>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }

        /// <summary>
        /// Shuffles the elements in the list using the Durstenfeld implementation of the Fisher-Yates algorithm.
        /// This method modifies the input list in-place, ensuring each permutation is equally likely, and returns the list for method chaining.
        /// Reference: http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        /// <param name="list">The list to be shuffled.</param>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <returns>The shuffled list.</returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            rng ??= new Random();
            var count = list.Count;
            while (count > 1)
            {
                --count;
                var index = rng.Next(count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }

            return list;
        }
    }
}