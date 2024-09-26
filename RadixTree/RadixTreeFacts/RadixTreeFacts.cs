using Xunit;

namespace RadixTreeStructure.Facts
{
    public class RadixTreeFacts
    {
        [Fact]
        public void InsertWord()
        {
            RadixTree<char> tree = new RadixTree<char>();
            tree.Add("hello");
            Assert.Equal(tree, new RadixTree<char> { "hello" });
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefix()
        {
            RadixTree<char> tree = new();
            tree.Add("hello");
            tree.Add("he");
            Assert.Equal(new RadixTree<char> { "hello", "he" }, tree);
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefixThatShouldSplit()
        {
            RadixTree<char> tree = new();
            tree.Add("water");
            tree.Add("waste");
            tree.Add("w");

            tree.Add("tear");
            Assert.Equal(new RadixTree<char> { "water", "waste", "w", "tear" }, tree);
        }

        [Fact]
        public void SearchForExistingWord()
        {
            RadixTree<char> tree = new();
            tree.Add(new List<char>() { 'h', 'e', 'l', 'l', 'o' });
            Assert.True(tree.Search(new List<char>() { 'h', 'e', 'l', 'l', 'o' }));
        }

        [Fact]
        public void SearchForNonExistingWord()
        {
            RadixTree<char> tree = new();
            var word = new char[] { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(word);
            var anotherWord = new char[] { 'a', 'b' };
            Assert.False(tree.Search(anotherWord));
        }

        [Fact]
        public void SearchForExistingPrefix()
        {
            RadixTree<char> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            var secondWord = new List<char>() { 'h', 'e', 'r', 'e' };
            tree.Add(secondWord);
            var prefix = new List<char>() { 'h', 'e' };
            Assert.True(tree.Search(prefix));
        }

        [Fact]
        public void DeleteExistingNode()
        {
            RadixTree<char> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            tree.Remove(firstWord);
            Assert.False(tree.Search(firstWord));
        }

        [Fact]
        public void DeleteNonLeafNode()
        {
            RadixTree<char> tree = new();
            tree.Add("abc");
            tree.Add("abcd");
            tree.Remove("abc");
            Assert.True(tree.Search("abc"));
        }

        [Fact]
        public void DeleteNonExistingNode()
        {
            RadixTree<char> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            var nonExistingWord = new List<char>() { 'h', 'e' };
            tree.Remove(nonExistingWord);
            Assert.False(tree.Search(nonExistingWord));
        }

        [Fact]
        public void CreateNewTreeWithIntegers()
        {
            RadixTree<int> tree = new();
            int[] integers = { 1, 2, 3 };
            tree.Add(integers);
            Assert.True(tree.Search(integers));
            int[] anotherSetOfIntegers = { 1, 5, 6 };
            tree.Add(anotherSetOfIntegers);
            Assert.Equal(tree, new RadixTree<int> { new[] { 1, 2, 3 }, new[] { 1, 5, 6 } });
        }
    }
}