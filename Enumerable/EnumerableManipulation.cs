#pragma warning disable S2589
using System;
using System.Collections.Generic;

namespace EnumerableTask
{
    public class EnumerableManipulation
    {
        /// <summary> Transforms all strings to upper case.</summary>
        /// <param name="data">Source string sequence.</param>
        /// <returns>
        ///   Returns sequence of source strings in uppercase.
        /// </returns>
        /// <example>
        ///    {"a", "b", "c"} => { "A", "B", "C" }
        ///    { "A", "B", "C" } => { "A", "B", "C" }
        ///    { "a", "A", "", null } => { "A", "A", "", null }.
        /// </example>
        public IEnumerable<string> GetUppercaseStrings(IEnumerable<string>? data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    yield return item is null ? item! : item.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary> Transforms an each string from sequence to its length.</summary>
        /// <param name="data">Source strings sequence.</param>
        /// <returns>
        ///   Returns sequence of strings length.
        /// </returns>
        /// <example>
        ///   { } => { }
        ///   {"a", "aa", "aaa" } => { 1, 2, 3 }
        ///   {"aa", "bb", "cc", "", "  ", null } => { 2, 2, 2, 0, 2, 0 }.
        /// </example>
        public IEnumerable<int> GetStringsLength(IEnumerable<string>? data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    yield return item is null ? 0 : item.Length;
                }
            }
        }

        /// <summary>Transforms integer sequence to its square sequence, f(x) = x * x. </summary>
        /// <param name="data">Source int sequence.</param>
        /// <returns>
        ///   Returns sequence of squared items.
        /// </returns>
        /// <example>
        ///   { } => { }
        ///   { 1, 2, 3, 4, 5 } => { 1, 4, 9, 16, 25 }
        ///   { -1, -2, -3, -4, -5 } => { 1, 4, 9, 16, 25 }.
        /// </example>
        public IEnumerable<long> GetSquareSequence(IEnumerable<int>? data)
        {
            if (data != null)
            {
                foreach (long item in data)
                {
                    yield return item * item;
                }
            }
        }

        /// <summary> Filters a string sequence by a prefix value (case insensitive).</summary>
        /// <param name="data">Source string sequence.</param>
        /// <param name="prefix">Prefix value to filter.</param>
        /// <returns>
        ///  Returns items from data that started with required prefix (case insensitive).
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when prefix is null.</exception>
        /// <example>
        ///  { "aaa", "bbbb", "ccc", null }, prefix = "b"  =>  { "bbbb" }
        ///  { "aaa", "bbbb", "ccc", null }, prefix = "B"  =>  { "bbbb" }
        ///  { "a","b","c" }, prefix = "D"  => { }
        ///  { "a","b","c" }, prefix = ""   => { "a","b","c" }
        ///  { "a","b","c", null }, prefix = ""   => { "a","b","c" }
        ///  { "a","b","c" }, prefix = null => ArgumentNullException.
        /// </example>
        public IEnumerable<string> GetPrefixItems(IEnumerable<string>? data, string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix), "Can't be null.");
            }

            return GetPrefixItems();

            IEnumerable<string> GetPrefixItems()
            {
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item != null && item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        {
                            yield return item;
                        }
                    }
                }
            }
        }

        /// <summary> Finds the 3 largest numbers from a sequence.</summary>
        /// <param name="data">Source sequence.</param>
        /// <returns>
        ///   Returns the 3 largest numbers from a sequence.
        /// </returns>
        /// <example>
        ///   { } => { }
        ///   { 1, 2 } => { 2, 1 }
        ///   { 1, 2, 3 } => { 3, 2, 1 }
        ///   { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } => { 10, 9, 8 }
        ///   { 10, 10, 10, 10 } => { 10, 10, 10 }.
        /// </example>
        public IEnumerable<int> Get3LargestItems(IEnumerable<int>? data)
        {
            var count = (int.MinValue, int.MinValue, int.MinValue);
            foreach (var item in data!)
            {
                count = item switch
                {
                    int i when i > count.Item1 => (i, count.Item1, count.Item2),
                    int i when i > count.Item2 => (count.Item1, i, count.Item2),
                    int i when i > count.Item3 => (count.Item1, count.Item2, i),
                    _ => count
                };
            }

            if (count.Item1 != int.MinValue)
            {
                yield return count.Item1;
            }

            if (count.Item2 != int.MinValue)
            {
                yield return count.Item2;
            }

            if (count.Item3 != int.MinValue)
            {
                yield return count.Item3;
            }
        }

        /// <summary> Calculates sum of all integers from object array.</summary>
        /// <param name="data">Source array.</param>
        /// <returns>
        ///    Returns the sum of all integers from object array.
        /// </returns>
        /// <example>
        ///    { 1, true, "a", "b", false, 1 } => 2
        ///    { true, false } => 0
        ///    { 10, "ten", 10 } => 20
        ///    { } => 0.
        /// </example>
        public int GetSumOfAllIntegers(object[] data)
        {
            var sum = 0;
            foreach (var item in data)
            {
                sum += item switch
                {
                    int i => i,
                    _ => 0
                };
            }

            return sum;
        }
    }
}
