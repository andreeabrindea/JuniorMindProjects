using System.Collections;

namespace DataStructures;

public class ObjectArrayEnumerator : IEnumerator
{
    private readonly ObjectArray arrayOfObjects;
    private int index;

    public ObjectArrayEnumerator(ObjectArray array, int index = -1)
    {
        this.arrayOfObjects = array;
        this.index = index;
    }

    public object Current
    {
        get
        {
            try
            {
                return arrayOfObjects[index];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public bool MoveNext()
    {
        index++;
        return index < arrayOfObjects.Count;
    }

    public void Reset()
    {
        index = -1;
    }
}