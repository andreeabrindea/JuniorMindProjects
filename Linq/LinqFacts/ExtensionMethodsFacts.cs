using Xunit;

namespace Linq.Facts;

public class ExtensionMethodsFacts
{
    [Fact]
    public void All_SourceIsNull_ShouldThrowException()
    {
        List<int> source = null;
        Assert.Throws<ArgumentNullException>(() => source.All(i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfIntegerEven_ShouldReturnFalse()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(numbers.All(i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfIntegersUnder10_ShouldReturnTrue()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(numbers.All(i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnTrue()
    {
        List<string> words = new List<string> { "abc", "air", "apple" };
        Assert.True(words.All(s => s.StartsWith('a')));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnFalse()
    {
        List<string> words = new List<string> { "abc", "air", "apple", "hello" };
        Assert.False(words.All(s => s.StartsWith('a')));
    }

    [Fact]
    public void Any_ListOfIntegerEven_ShouldReturnTrue()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.True(numbers.Any(i => i % 2 == 0));
    }

    [Fact]
    public void Any_ListOfIntegerEven_ShouldReturnFalse()
    {
        List<int> numbers = new List<int> { 1, 3, 5, 7 };
        Assert.False(numbers.Any(i => i % 2 == 0));
    }

    [Fact]
    public void Any_ListOfStringsStartsWithA_ShouldReturnTrue()
    {
        List<string> words = new List<string> { "abc", "air", "apple", "hello" };
        Assert.True(words.Any(s => s.StartsWith('a')));
    }

    [Fact]
    public void Any_ListOfStringsStartsWithA_ShouldReturnFalse()
    {
        List<string> words = new List<string> { "bcd", "cdef", "hello" };
        Assert.False(words.Any(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_ElementThatStartsWithA()
    {
        List<string> words = new List<string> { "bcd", "air", "hello" };
        Assert.Equal("air",words.First(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_ElementThatStartsWithA_WhenThereIsNoElement()
    {
        List<string> words = new List<string> { "bcd", "cdef", "hello" };
        Assert.Throws<InvalidOperationException>(() => words.First(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_EvenElement_WhenThereIsNoElement()
    {
        List<int> numbers = new List<int> {1, 7, 5, 9, 21, 73};
        Assert.Throws<InvalidOperationException>(() => numbers.First(i => i % 2 == 0));
    }

    [Fact]
    public void First_EvenElement()
    {
        List<int> numbers = new List<int> {1, 7, 8, 9, 20, 2, 4};
        Assert.Equal(8, numbers.First(i => i % 2 == 0));
    }

    [Fact]
    public void Select_DoubleTheElements()
    {
        List<int> numbers = new List<int> {1, 2, 3};
        Assert.Equal(new List<int> { 2, 4, 6 }, numbers.Select(i => i * 2));
    }

    [Fact]
    public void Select_ElementsThatStartWithA()
    {
        List<string> words = new List<string> {"abc","def","ghi"};
        Assert.Equal(new List<bool> { true, false, false }, words.Select(s => s.StartsWith("a")));
    }

    [Fact]
    public void SelectMany_DoubleEachElement()
    {
        List<List<int>> numbers = new List<List<int>>
        {
            new() { 1, 2, 3 },
            new() { 4, 5, 6 },
            new() { 7, 8, 9 },
        };
        Assert.Equal(
            new List<int> { 2, 4, 6, 8, 10, 12, 14, 16, 18 },
            numbers.SelectMany(i => i.Select(x => x * 2)));
    }

    [Fact]
    public void Where_ElementIsEven()
    {
        List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
        Assert.Equal(new List<int> { 2, 4, 6, 8 }, numbers.Where(i => i % 2 == 0));
    }

    [Fact]
    public void Where_ThereIsNoElementThatRespectsThePredicate()
    {
        List<int> numbers = new() { 1, 3, 5, 7};
        Assert.Equal(new List<int> { }, numbers.Where(i => i % 2 == 0));
    }

    [Fact]
    public void ToDictionary_KeyIsDoubleElementSelector()
    {
        List<int> numbers = new() { 1, 3, 5};
        Assert.Equal(
            new Dictionary<int, int> { { 2, 1 }, { 6, 3 }, { 10, 5 } },
            numbers.ToDictionary(i => i * 2, y => y));
    }

    [Fact]
    public void ToDictionary_ListIsEmpty()
    {
        List<int> numbers = new() { };
        Assert.Equal(new Dictionary<int, int> { }, numbers.ToDictionary(i => i * 2, y => y + 2));
    }

    [Fact]
    public void Zip_WhenListsHaveTheSameNoOfElements()
    {
        List<int> oddNumbers = new List<int> { 1, 3, 5 };
        List<int> evenNumbers = new List<int> { 2, 4, 6 };
        Assert.Equal(new List<int> { 3, 7, 11 }, oddNumbers.Zip(evenNumbers, (a, b) => a + b));
    }

    [Fact]
    public void Zip_WhenListsHaveDifferentNoOfElements()
    {
        List<int> oddNumbers = new List<int> { 1, 3, 5 };
        List<int> evenNumbers = new List<int> { 2, 4, 6, 8 };
        Assert.Equal(new List<int> { 3, 7, 11 }, oddNumbers.Zip(evenNumbers, (a, b) => a + b));
    }

    [Fact]
    public void Zip_OneListIsEmpty()
    {
        List<int> empty = new List<int> { };
        List<int> evenNumbers = new List<int> { 2, 4, 6, 8 };
        Assert.Equal(new List<int> { }, empty.Zip(evenNumbers, (a, b) => a + b));
    }

    [Fact]
    public void Aggregate_SumOfTheElements()
    {
        List<int> numbers = new() { 1, 2, 3 };
        int seed = 0;
        Assert.Equal(6, numbers.Aggregate(seed, (s, a) => s + a));
    }

    [Fact]
    public void Aggregate_ListIsEmpty()
    {
        List<int> empty = new() { };
        int seed = 0;
        Assert.Equal(0, empty.Aggregate(seed, (s, a) => s + a));
    }

    [Fact]
    public void Aggregate_ListIsNull()
    {
        List<int> list = null;
        int seed = 0;
        Assert.Throws<ArgumentNullException>(() => list.Aggregate(seed, (s, a) => s + a));
    }

    [Fact]
    public void Aggregate_FuncIsNull()
    {
        List<int> empty = new() { };
        int seed = 0;
        Assert.Throws<ArgumentNullException>(() => empty.Aggregate(seed, null));
    }

    [Fact]
    public void Join_ElementsThatAppearInBothLists()
    {
        List<int> outer = new() { 2, 4, 6, 12 };
        List<int> inner = new() { 4, 8, 12 };
        Assert.Equal(
            new List<int> { 4, 12 },
            outer.Join(inner, i => i, i => i, (i, _) => i));
    }

    [Fact]
    public void Join_OneListIsEmpty()
    {
        List<int> outer = new() { };
        List<int> inner = new() { 4, 8, 12 };
        Assert.Equal(
            new List<int> { },
            outer.Join(inner, i => i, i => i, (i, _) => i));
    }

    [Fact]
    public void Join_OneFunctionIsNull()
    {
        List<int> outer = new() { 2, 5, 6 };
        List<int> inner = new() { 4, 8, 12 };
        Assert.Throws<ArgumentNullException>(
            () => outer.Join(inner, null, i => i, (i, _) => i).ToList());
    }

    [Fact]
    public void Distinct_RemoveDuplicatesFromListOfIntegers()
    {
        List<int> list = new() { 1, 2, 2, 1, 4, 5, 6, 4, 4, 2, 2, 3, 5 };
        Assert.Equal(new List<int>() { 1, 2, 4, 5, 6, 3 }, list.Distinct(EqualityComparer<int>.Default));
    }

    [Fact]
    public void Distinct_RemoveDuplicatesFromListOfStrings()
    {
        List<string> list = new() { "hello", "there", "hello", "hi", "there", "hi" };
        Assert.Equal(new List<string>() { "hello", "there", "hi" }, list.Distinct(EqualityComparer<string>.Default));
    }

    [Fact]
    public void Distinct_ListIsNull_ShouldThrowException()
    {
        List<string> list = null;
        Assert.Throws<ArgumentNullException>(() => list.Distinct(EqualityComparer<string>.Default).ToList());
    }

    [Fact]
    public void Distinct_ComparerIsNull_ShouldThrowException()
    {
        List<string> list = new() { "hello", "there", "hello", "hi", "there", "hi" };
        Assert.Throws<ArgumentNullException>(() => list.Distinct(null).ToList());
    }

    [Fact]
    public void Union_ListsOfInteger()
    {
        List<int> list = new() { 1, 2, 2, 1, 4, 5, 6, 4, 4, 2, 2, 3, 5 };
        List<int> list2 = new() { 11, 12, 11, 13, 12, 15, 11, 13, 14, 11 };
        Assert.Equal(new List<int>() { 1, 2, 4, 5, 6, 3, 11, 12, 13, 15, 14 }, list.Union(list2, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Union_FirstListIsEmpty()
    {
        List<int> list = new();
        List<int> list2 = new() { 11, 12, 11, 13, 12, 15, 11, 13, 14, 11 };
        Assert.Equal(new List<int>() { 11, 12, 13, 15, 14 }, list.Union(list2, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Union_FistListIsNull()
    {
        List<int> list = null;
        List<int> list2 = new() { 11, 12, 11, 13, 12, 15, 11, 13, 14, 11 };
        Assert.Throws<ArgumentNullException>(() => list.Union(list2, EqualityComparer<int>.Default).ToList());
    }

    [Fact]
    public void Intersect_ListsOfIntegers()
    {
        List<int> first = new() { 1, 2, 3, 4, 5, 6, 1, 4, 5, 6 };
        List<int> second = new() { 1, 2, 3, 7, 9, 2, 1 };
        Assert.Equal(new List<int> { 1, 2, 3 }, first.Intersect(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Intersect_FirstListIsEmpty()
    {
        List<int> first = new() { };
        List<int> second = new() { 1, 2, 3, 7, 9, 2, 1 };
        Assert.Equal(new List<int> { }, first.Intersect(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Intersect_ThereAreNoCommonElements()
    {
        List<int> first = new() { 12, 34, 56};
        List<int> second = new() { 1, 2, 3, 7, 9, 2, 1 };
        Assert.Equal(new List<int> { }, first.Intersect(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Intersect_FirstListIsNull()
    {
        List<int> first = null;
        List<int> second = new() { 1, 2, 3, 7, 9, 2, 1 };
        Assert.Throws<ArgumentNullException>(() => first.Intersect(second, EqualityComparer<int>.Default).ToList());
    }

    [Fact]
    public void Intersect_ComparerIsNull()
    {
        List<int> first = new() { 1, 2, 4};
        List<int> second = new() { 1, 2, 3, 7, 9, 2, 1 };
        Assert.Throws<ArgumentNullException>(() => first.Intersect(second, null).ToList());
    }

    [Fact]
    public void Except_ListsOfIntegers()
    {
        List<int> first = new() { 1, 2, 3, 4 };
        List<int> second = new() { 2, 4, 5, 6, 7 };
        Assert.Equal(new List<int>() { 1, 3 }, first.Except(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Except_ListsAreIdentical()
    {
        List<int> first = new() { 1, 2, 3, 4 };
        List<int> second = new() { 1, 2, 3, 4 };
        Assert.Equal(new List<int>() { }, first.Except(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Except_FirstListIsEmpty()
    {
        List<int> first = new() {  };
        List<int> second = new() { 2, 4, 5, 6, 7 };
        Assert.Equal(new List<int>() {  }, first.Except(second, EqualityComparer<int>.Default));
    }

    [Fact]
    public void Except_FirstListIsNull()
    {
        List<int> first = null;
        List<int> second = new() { 2, 4, 5, 6, 7 };
        Assert.Throws<ArgumentNullException>(() => first.Except(second, EqualityComparer<int>.Default).ToList());
    }

    [Fact]
    public void Except_ComparerIsNull()
    {
        List<int> first = new() { 1, 2, 3};
        List<int> second = new() { 2, 4, 5, 6, 7 };
        Assert.Throws<ArgumentNullException>(() => first.Except(second, null).ToList());
    }
}