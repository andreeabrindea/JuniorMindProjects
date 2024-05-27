namespace Exceptions;

public static class Exceptions
{
    public static void Main()
    {
        Division(17, 0);
        int[] array = new int[2];
        Add(array, 1);
        Console.WriteLine(Square(999999999));
        Console.WriteLine(FirstChar(null));

    }

    public static void Division(int a, int b)
    {
        int result = 0;
        try
        {
            result = a / b;
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message + " " + e.Source);
        }
        finally
        {
            Console.WriteLine(result);
        }

    }

    public static double DivisionUncatched(int a, int b)
    {
        return a / b;
    }

    public static void Add(int[] array, int element)
    {
        try
        {
            array[array.Length] = element;
        }
        catch (IndexOutOfRangeException e)
        {
            Array.Resize(ref array, array.Length * 2);
            Console.WriteLine(e.Message + " " + e.Source);
            throw;
        }
    }

    public static int Square(int a)
    {
        try
        {
            return checked(a * a);
        }
        catch (OverflowException e)
        {
            Console.WriteLine(e.Message + " " + e.Source);
            return 0;
        }
    }

    public static char FirstChar(string input)
    {
        try
        {
            return input[0];
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.Message + " " + e.Source);
            return '\0';
        }
    }
}