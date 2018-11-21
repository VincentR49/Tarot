using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeList<T> : ScriptableObject
{
    public List<T> Items = new List<T>();
	public int Count => Items.Count;
	
    public void Add(T t)
    {
        if (!Items.Contains(t))
        {
            Items.Add(t);
        }
    }
	
	
    public void Remove(T t)
    {
        if (Items.Contains(t))
        {
            Items.Remove(t);
        }
    }
	
	
	public void Clear()
	{
		if (Items.Count > 0)
		{
			Items.Clear();
		}
	}
}