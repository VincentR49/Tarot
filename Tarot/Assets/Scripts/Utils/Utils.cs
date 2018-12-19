using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using System;

// Generic methods
public static class Utils  
{
    // Generate a list of unique indexes between min and max
    public static List<int> GetRandomUniqueIndexes(int n, int min, int max)
    {
        System.Random rnd = new System.Random();
        List<int> indexes = new List<int>();
        List<int> possibleIndexes = Enumerable.Range(min, max).ToList();
        for (int i = 0; i < n; i++)
        {
            int index = possibleIndexes[rnd.Next(0, possibleIndexes.Count - 1)];
            indexes.Add(index);
            possibleIndexes.Remove(index); // to have unique indexes
        }
        return indexes;
    }
	
	// Extension method to get Description of an enum
	public static string GetDescription(this Enum value)
	{
		Type type = value.GetType();
		string name = Enum.GetName(type, value);
		if (name != null)
		{
			FieldInfo field = type.GetField(name);
			if (field != null)
			{
				DescriptionAttribute attr = 
					   Attribute.GetCustomAttribute(field, 
						 typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attr != null)
				{
					return attr.Description;
				}
			}
		}
		return null;
	}
}
