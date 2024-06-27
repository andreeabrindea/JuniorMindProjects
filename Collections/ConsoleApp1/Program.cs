using Collections;

HashTableDictionary<int, string> dictionary = new(5)
{
    { 2, "a" },
    { 3, "b" },
    { 4, "c" },
    { 5, "d" },
    { 6, "e" }
};
        
KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[5];
dictionary.CopyTo(array, 0);
for(int i =0; i< array.Length; i++)
{
    Console.WriteLine(array[i]);
}