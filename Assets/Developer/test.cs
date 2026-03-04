using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Start()
    {
        List<ulong> orderedIds = new List<ulong>()
        {
            0, 1, 2, 3, 4, 5, 6, 7
        };

        int i = orderedIds.Count - 1;

        for (int j = 0; j < orderedIds.Count; j++)
        {
            Debug.Log($"{orderedIds[j]}: {((i - j) * (i - j)) + 1}");
        }   
    }
}
