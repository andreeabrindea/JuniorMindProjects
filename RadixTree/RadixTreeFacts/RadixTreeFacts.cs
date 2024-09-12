using Xunit;

namespace RadixTreeStructure.Facts
{
    public class RadixTreeFacts
    {
        [Fact]
        public void InsertWord()
        {
            RadixTree<string> tree = new RadixTree<string>();
            tree.Add("hello");
            Assert.Equal(tree, new RadixTree<string> { "hello" });
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefix()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            tree.Add("he");
            Assert.Equal(new RadixTree<string> { "hello", "he" }, tree);
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefixThatShouldSplit()
        {
            RadixTree<string> tree = new();
            tree.Add("water");
            tree.Add("waste");
            tree.Add("w");

            Assert.Equal(new RadixTree<string> { "water", "waste", "w" }, tree);
        }

        [Fact]
        public void SearchForExistingWord()
        {
            RadixTree<List<char>> tree = new();
            tree.Add(new List<char>() { 'h', 'e', 'l', 'l', 'o' });
            Assert.True(tree.Search(new List<char>() { 'h', 'e', 'l', 'l', 'o' }));
        }

        [Fact]
        public void SearchForNonExistingWord()
        {
            RadixTree<char[]> tree = new();
            var word = new char[] { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(word);
            var anotherWord = new char[] { 'a', 'b' };
            Assert.False(tree.Search(anotherWord));
        }

        [Fact]
        public void SearchForExistingPrefix()
        {
            RadixTree<List<char>> tree = new();
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
            RadixTree<List<char>> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            tree.Remove(firstWord);
            Assert.False(tree.Search(firstWord));
        }

        [Fact]
        public void DeleteNonExistingNode()
        {
            RadixTree<List<char>> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            var nonExistingWord = new List<char>() { 'h', 'e' };
            tree.Remove(nonExistingWord);
            Assert.False(tree.Search(nonExistingWord));
        }
    }
}