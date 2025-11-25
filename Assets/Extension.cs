using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public static class Extension
{
    public static List<T> Shuffle<T>(this List<T> dataList)
    {
        List<T> result = new List<T>(dataList);

        for (int i = 0; i < dataList.Count; i++)
        {
            int randomIndex = Random.Range(0, dataList.Count());
            T randomData = result[randomIndex];
            T currentData = result[i];

            result[i] = randomData;
            result[randomIndex] = currentData; 
        }

        return result;
    }

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



