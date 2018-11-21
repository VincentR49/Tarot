using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeStack<T> : ScriptableObject
{
    public List<T> Items = new List<T>();
	public int Count => Items.Count;
	
    public void Push(T t)
    {
        if (!Items.Contains(t))
        {
            Items.Add(t);
        }
    }
	
	
    public T Pop()
    {
        T t = default(T);
        if (Items.Count > 0)
        {
            t = Items[Items.Count - 1];
            Items.Remove(t);
        }
        return t;
    }
 
 
	public void Clear()
	{
		if (Items.Count > 0)
		{
			Items.Clear();
		}
	}
}