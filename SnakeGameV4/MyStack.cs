using System.Text;
using System.Collections;

internal class MyStack<T> : IEnumerable<T>
{
    private T[] arr;
    private int len = 0; // length user thinks array is
    private int capacity = 0; // Actual array size

    internal MyStack()
    {
        arr = new T[4];
    }

    public MyStack(int capacity)
    {
        if (capacity < 0) throw new ArgumentException("Illegal Capacity: " + capacity);
        this.capacity = capacity;
        arr = new T[capacity];
    }


    public int Size()
    {
        return len;
    }

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    public T Get(int index)
    {
        return arr[index];
    }

    public void Set(int index, T elem)
    {
        arr[index] = elem;
    }

    public void Clear()
    {
        for (int i = 0; i < len; i++) arr[i] = default(T);
        len = 0;
    }

    public void Add(T elem)
    {
        
        if (len + 1 >= capacity)
        {
            if (capacity == 0) capacity = 1;
            else capacity *= 2; 
            T[] new_arr = new T[capacity];
            for (int i = 0; i < len; i++) new_arr[i] = arr[i];
            arr = new_arr; 
        }

        arr[len++] = elem;
    }


    public T RemoveAt(int rm_index)
    {
        if (rm_index >= len || rm_index < 0) throw new IndexOutOfRangeException();
        T data = arr[rm_index];
        T[] new_arr = (T[])new T[len - 1];
        for (int i = 0, j = 0; i < len; i++, j++)
            if (i == rm_index) j--; 
            else new_arr[j] = arr[i];
        arr = new_arr;
        capacity = --len;
        return data;
    }

    public bool Remove(object obj)
    {
        int index = IndexOf(obj);
        if (index == -1) return false;
        RemoveAt(index);
        return true;
    }

   
    public int IndexOf(object obj)
    {
        for (int i = 0; i < len; i++)
        {
            if (obj == null)
            {
                if (arr[i] == null) return i;
            }
            else
            {
                if (obj.Equals(arr[i])) return i;
            }
        }
        return -1;
    }

    public bool Contains(object obj)
    {
        return IndexOf(obj) != -1;
    }
    public override string ToString()
    {
        if (len == 0) return "[]";
        else
        {
            StringBuilder sb = new StringBuilder(len).Append("[");
            for (int i = 0; i < len - 1; i++) sb.Append(arr[i] + ", ");
            return sb.Append(arr[len - 1] + "]").ToString();
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)arr).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return arr.GetEnumerator();
    }
}