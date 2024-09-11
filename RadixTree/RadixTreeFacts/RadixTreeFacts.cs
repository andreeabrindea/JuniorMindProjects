using Xunit;

namespace RadixTreeStructure.Facts
{
    public class RadixTreeFacts
    {
        [Fact]
        public void InsertWord()
        {
            RadixTree<List<char>> tree = new RadixTree<List<char>>();
            List<char> words = new List<char> { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(words);
            Assert.Equal(tree, new RadixTree<List<char>> { new List<char> { 'h', 'e', 'l', 'l', 'o' }});
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefix()
        {
            RadixTree<List<char>> tree = new();
            var firstWord = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(firstWord);
            var secondWord = new List<char>() { 'h', 'e' };
            tree.Add(secondWord);
            Assert.Equal(new RadixTree<List<char>> { firstWord, secondWord }, tree);
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefixThatShouldSplit()
        {
            RadixTree<List<char>> tree = new();
            tree.Add(new List<char>() { 'w', 'a', 't', 'e', 'r' });
            tree.Add(new List<char>() { 'w', 'a', 's', 't', 'e' });
            tree.Add(new List<char>() { 'w' });

            Assert.Equal(new RadixTree<List<char>> { new List<char>() { 'w', 'a', 't', 'e', 'r' }, new List<char>() { 'w', 'a', 's', 't', 'e' }, new List<char>() { 'w' } }, tree);
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
            RadixTree<List<char>> tree = new();
            var word = new List<char>() { 'h', 'e', 'l', 'l', 'o' };
            tree.Add(word);
            var anotherWord = new List<char>() { 'a', 'b' };
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