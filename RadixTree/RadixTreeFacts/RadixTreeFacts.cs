using Xunit;

namespace RadixTreeStructure.Facts
{
    public class RadixTreeFacts
    {
        [Fact]
        public void InsertWord()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            Assert.Equal(new RadixTree<string> { "hello" }, tree);
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
            RadixTree<string> tree = new();
            tree.Add("hello");
            Assert.True(tree.Search("hello"));
        }

        [Fact]
        public void SearchForNonExistingWord()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            Assert.False(tree.Search("abc"));
        }

        [Fact]
        public void SearchForExistingPrefix()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            tree.Add("here");
            Assert.True(tree.Search("he"));
        }

        [Fact]
        public void DeleteExistingNode()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            tree.Remove("hello");
            Assert.False(tree.Search("hello"));
        }

        [Fact]
        public void DeleteNonExistingNode()
        {
            RadixTree<string> tree = new();
            tree.Add("hello");
            tree.Remove("he");
            Assert.False(tree.Search("he"));
        }
    }
}