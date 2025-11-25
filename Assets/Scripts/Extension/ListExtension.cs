using System.Collections.Generic;
using UnityEngine;


public static class ListExtension
{
    /// <summary>
    /// Returns a new shuffled copy of the source list
    /// Does NOT modify the original list.
    /// </summary>
    public static List<T> Shuffle<T>(this List<T> dataList)
    {
        List<T> result = new List<T>(dataList);

        for (int i = 0; i < dataList.Count; i++)
        {
            int randomIndex = Random.Range(0, dataList.Count);

            //Swaps the data 
            (result[i], result[randomIndex]) = (result[randomIndex], result[i]);
        }

        return result;
    }

    /// <summary>
    /// Creates a list of items where each item appears exactly twice,
    /// then shuffles the result.
    ///
    /// Example: source = [A,B,C], totalCount = 4 → result could be [B,A,A,B].
    /// </summary>
    public static List<T> RandomDuplicate<T>(this List<T> dataList, int totalCount)
    {
        List<T> duplicatedList = new List<T>();
        int count = (int)(totalCount * .5f);

        for (int i = 0; i < count; i++)
        {
            T data = dataList[i];

            // Adding twice for duplication
            duplicatedList.Add(data);
            duplicatedList.Add(data);
        }

        return duplicatedList.Shuffle();
    }
}



