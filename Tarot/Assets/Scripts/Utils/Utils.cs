using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Generic methods
public static class Utils  {

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
}
