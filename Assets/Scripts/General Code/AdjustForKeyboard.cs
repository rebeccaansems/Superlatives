using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustForKeyboard : MonoBehaviour
{
    private Vector3 oriPos;
    private bool keyboardWasVisible = false;

    private void Start()
    {
        oriPos = this.transform.position;
    }

    private void Update()
    {
        if (TouchScreenKeyboard.visible && keyboardWasVisible == false)
        {
            this.transform.position += new Vector3(0, TouchScreenKeyboard.area.height, 0);
            keyboardWasVisible = true;
        }
        else if (keyboardWasVisible)
        {
            this.transform.position = oriPos;
            keyboardWasVisible = false;
        }
    }

}
