using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringStorage : MonoBehaviour
{
    public string[] strings;
    public string GetString(int index)
    {
        return strings[index];
    }
    public int GetLength()
    {
        return strings.Length;
    }
}
