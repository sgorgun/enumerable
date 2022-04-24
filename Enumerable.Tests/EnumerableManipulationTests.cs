using NUnit.Framework;

namespace EnumerableTask.Tests
{
    [TestFixture]
    public class EnumerableManipulationTests
    {
        private static IEnumerable<TestCaseData> DataCasesForGetUpperCaseStringTest
        {
            get
            {
                yield return new TestCaseData(
                    new[] { "a", "b", "c" },
                    new[] { "A", "B", "C" },
                    "Method should return upper cased source strings.");
                yield return new TestCaseData(
                    new[] { "A", "B", "C" },
                    new[] { "A", "B", "C" },
                    "Method should return source strings.");
                yield return new TestCaseData(
                    new[] { "a", "A", string.Empty, null },
                    new[] { "A", "A", string.Empty, null },
                    "Method should not transform empty strings, upper string and nulls.");
                yield return new TestCaseData(
                    new[] { "a", "aa", "aaa", "aaaa", "aaaaa" },
                    new[] { "A", "AA", "AAA", "AAAA", "AAAAA" },
                    "Method should return upper cased source strings.");
                yield return new TestCaseData(
                    new[] { "a", "aA", "Aaa", "aAaa", "Aaaaa", "A horse, a kingdom for a horse!" },
                    new[] { "A", "AA", "AAA", "AAAA", "AAAAA", "A HORSE, A KINGDOM FOR A HORSE!" },
                    "Method should return upper cased source strings.");
                yield return new TestCaseData(
                    new[] { string.Empty, string.Empty, null, null, "     ", "AAA" },
                    new[] { string.Empty, string.Empty, null, null, "     ", "AAA" },
                    "Method should not transform empty strings, upper string and nulls.");
            }
        }

        private static IEnumerable<TestCaseData> DataCasesForGetStringsLengthTest
        {
            get
            {
                yield return new TestCaseData(
                    new[] { "a", "aa", "aaa", "aaaa", "aaaaa" },
                    new[] { 1, 2, 3, 4, 5 },
                    "Method should return strings length.");
                yield return new TestCaseData(
                    new[] { "a", "a", "a", string.Empty, string.Empty },
                    new[] { 1, 1, 1, 0, 0 },
                    "Method should return strings length (empty strings).");
                yield return new TestCaseData(
                    new[] { "\u00ba", "\u00ba\u00bb", "\u00ba\u00bb\u00bc", "\u00ba\u00bb\u00bc\u00bd" },
                    new[] { 1, 2, 3, 4 },
                    "Method should return strings (with Unicode symbols) length.");
                yield return new TestCaseData(
                    new[] { "a", "a", "a", null, null },
                    new[] { 1, 1, 1, 0, 0 },
                    "Method should return strings length (null).");
                yield return new TestCaseData(
                    new[] { " ", "  ", "   " },
                    new[] { 1, 2, 3 },
                    "Method should return strings length for whitespace strings (Length(\" \")=1, Length(\"  \")=2).");
            }
        }

        private static IEnumerable<TestCaseData> DataCasesForGetSquareSequenceTest
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 },
                    new long[] { 100, 400, 900, 1600, 2500, 3600, 4900, 6400, 8100, 10000 },
                    "Method should return squares of source sequence.");
                yield return new TestCaseData(
                    new int[] { 1, -2, 3, -4, 5, -6, 7, -8, 9, -10 },
                    new long[] { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 },
                    "Method should return squares of source sequence.");
                yield return new TestCaseData(
                    new int[] { -10, -20, -30, -40, -50, -60, -70, -80, -90, -100 },
                    new long[] { 100, 400, 900, 1600, 2500, 3600, 4900, 6400, 8100, 10000 },
                    "Method should return squares of source sequence.");
                yield return new TestCaseData(
                    new int[] { 65536, 65537, 2147483647 },
                    new long[] { 4294967296, 4295098369, 4611686014132420609 },
                    "Method should return squares of source sequence for large numbers.");
                yield return new TestCaseData(
                    Array.Empty<int>(),
                    Array.Empty<long>(),
                    "Method should return empty sequence if source is empty.");
            }
        }

        private static IEnumerable<TestCaseData> DataCasesForGetPrefixItemsTest
        {
            get
            {
                string[] data = "A horse, a kingdom for a horse!".Split(' ');
                string?[] nulls = { null, null, null };
                string[] result = new string[] { "horse,", "horse!" };

                yield return new TestCaseData(
                    new[] { "aaa", "bbbb", "ccc", null },
                    "b",
                    new[] { "bbbb" },
                    "Method should return items started with required prefix.");
                yield return new TestCaseData(
                    new[] { "aaa", "aaabbbb", "cccbbbb", null },
                    "b",
                    Array.Empty<string>(),
                    "Method should return items started with required prefix.");

                yield return new TestCaseData(
                    data,
                    "horse",
                    result,
                    "Method should return items started with required prefix.");
                yield return new TestCaseData(
                    data,
                    "HORSE",
                    result,
                    "Method should return items started with required prefix (uppercase).");
                yield return new TestCaseData(
                    data.Concat(nulls),
                    "HORSE",
                    result,
                    "Method should return items started with required prefix (test with nulls).");
                yield return new TestCaseData(
                    data.Concat(nulls),
                    string.Empty,
                    data,
                    "Method should return all not null items if prefix is empty string.");
                yield return new TestCaseData(
                    data,
                    "cow",
                    Array.Empty<string>(),
                    "Method should return empty sequence if no items started with required prefix.");
            }
        }

        private static IEnumerable<TestCaseData> DataCasesForGet3LargestItemsTest
        {
            get
            {
                yield return new TestCaseData(
                    Enumerable.Range(1, 100).ToArray(),
                    new int[] { 100, 99, 98 },
                    "Method should return the 3 largest numbers.");
                yield return new TestCaseData(
                    new int[] { -1, 23, 56, 654, 1, 43, 0, -101, -23 },
                    new int[] { 654, 56, 43 },
                    "Method should return the 3 largest numbers.");
                yield return new TestCaseData(
                    new int[] { 1, 2 },
                    new int[] { 2, 1 },
                    "Method should return all items if source sequence consists of less than 3 items.");
                yield return new TestCaseData(
                    new int[] { 10, 10, 10, 10, 10 },
                    new int[] { 10, 10, 10 },
                    "Method should return all equal items if source sequence is monotone.");
                yield return new TestCaseData(
                    new int[] { 10, 10, 10, 10, 10, 20, 20, 20 },
                    new int[] { 20, 20, 20 },
                    "Method should return all equal items if source sequence contains more than 3 identical elements.");
                yield return new TestCaseData(
                    Array.Empty<int>(),
                    Array.Empty<int>(),
                    "Method should return empty sequence if source sequence is empty.");
            }
        }

        private static IEnumerable<TestCaseData> DataCasesForGetSumOfAllIntegersTest
        {
            get
            {
                yield return new TestCaseData(
                    new object[] { 1, true, "a", "b", false, 1 },
                    2,
                    "Method should return the sum of all integers.");
                yield return new TestCaseData(
                    new object[] { true, false },
                    0,
                    "Method should return zero if data does not contain integers.");
                yield return new TestCaseData(
                    new object?[] { 10, "ten", 10, null },
                    20,
                    "Method should return the sum of all integers.");
                yield return new TestCaseData(
                    new object?[] { 10, "ten", -10, null },
                    0,
                    "Method should return the sum of all integers.");
                yield return new TestCaseData(
                    Array.Empty<object>(),
                    0,
                    "Method should return zero if array is empty.");
                yield return new TestCaseData(
                    new object?[] { null, null, null },
                    0,
                    "Method should return zero if array contains only null elements.");
            }
        }

        [TestCaseSource(nameof(DataCasesForGetUpperCaseStringTest))]
        public void GetUppercaseStringsTests(IEnumerable<string> data, IEnumerable<string> expected, string message)
        {
            var task = new EnumerableManipulation();

            Assert.AreEqual(task.GetUppercaseStrings(data), expected, message: message);
        }

        [TestCaseSource(nameof(DataCasesForGetStringsLengthTest))]
        public void GetStringsLengthTests(IEnumerable<string> data, IEnumerable<int> expected, string message)
        {
            var target = new EnumerableManipulation();

            Assert.AreEqual(target.GetStringsLength(data), expected, message: message);
        }

        [TestCaseSource(nameof(DataCasesForGetSquareSequenceTest))]
        public void GetSquareSequenceTests(IEnumerable<int> data, IEnumerable<long> expected, string message)
        {
            var target = new EnumerableManipulation();

            CollectionAssert.AreEqual(target.GetSquareSequence(data), expected, message: message);
        }

        [TestCaseSource(nameof(DataCasesForGetPrefixItemsTest))]
        public void GetEvenItemsTests(IEnumerable<string> data, string prefix, IEnumerable<string> expected, string message)
        {
            var target = new EnumerableManipulation();

            CollectionAssert.AreEqual(target.GetPrefixItems(data, prefix), expected, message: message);
        }

        [Test]
        public void GetEvenItems_IfPrefixIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => new EnumerableManipulation().GetPrefixItems(Array.Empty<string>(), null!),
                message: "Prefix cannot be null.");

        [TestCaseSource(nameof(DataCasesForGet3LargestItemsTest))]
        public void Get3LargestItemsTests(IEnumerable<int> data, IEnumerable<int> expected, string message)
        {
            var target = new EnumerableManipulation();

            CollectionAssert.AreEqual(target.Get3LargestItems(data), expected, message: message);
        }

        [TestCaseSource(nameof(DataCasesForGetSumOfAllIntegersTest))]
        public void GetSumOfAllIntegersTests(object[] data, int expected, string message)
        {
            var target = new EnumerableManipulation();

            Assert.AreEqual(target.GetSumOfAllIntegers(data), expected, message: message);
        }
    }
}
