using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FixUITextSize : MonoBehaviour
{
    public int incrementSize = 20;

    void Start()
    {
        int currentSize = GetComponent<Text>().text.Length;
        if (currentSize < 28)
        {
            GetComponent<Text>().fontSize = 80;
        }
        else if (currentSize < 43)
        {
            GetComponent<Text>().fontSize = 65;
        }
        else if (currentSize < 58)
        {
            GetComponent<Text>().fontSize = 50;
        }
        else if (currentSize < 73)
        {
            GetComponent<Text>().fontSize = 40;
        }
    }
}
