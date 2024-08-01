using Xunit;

namespace RadixTreeStructure.Facts
{
    public class RadixTreeFacts
    {
        [Fact]
        public void InsertWord()
        {
            RadixTree tree = new();
            tree.Add("hello");
            Assert.Equal(new RadixTree { "hello" }, tree);
        }

        [Fact]
        public void InsertMultipleWordsWithSamePrefix()
        {
            RadixTree tree = new();
            tree.Add("hello");
            tree.Add("he");
            Assert.Equal(new RadixTree { "hello", "he" }, tree);
        }
        
        [Fact]
        public void InsertMultipleWordsWithSamePrefix2()
        {
            RadixTree tree = new();
            tree.Add("water");
            tree.Add("waste");
            tree.Add("w");
            Assert.Equal(new RadixTree { "water", "waste", "w" }, tree);
        }

        // [Fact]
        // public void SearchForExistingWord()
        // {
        //     RadixTree tree = new();
        //     tree.Add("hello");
        //     Assert.True(tree.Search("hello"));
        // }
        //
        // [Fact]
        // public void SearchForNonExistingWord()
        // {
        //     RadixTree tree = new();
        //     tree.Add("hello");
        //     Assert.False(tree.Search("abc"));
        // }
        //
        // [Fact]
        // public void SearchForExistingPrefix()
        // {
        //     RadixTree tree = new();
        //     tree.Add("hello");
        //     tree.Add("here");
        //     Assert.False(tree.Search("he"));
        // }
        //
        // [Fact]
        // public void DeleteExistingNode()
        // {
        //     RadixTree tree = new();
        //     tree.Add("hello");
        //     tree.Delete("hello");
        //     Assert.False(tree.Search("hello"));
        // }
        //
        // [Fact]
        // public void DeleteNonExistingNode()
        // {
        //     RadixTree tree = new();
        //     tree.Add("hello");
        //     tree.Delete("he");
        //     Assert.False(tree.Search("he"));
        // }
    }
}