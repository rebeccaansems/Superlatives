using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuperlativeQuestionsList
{
    public List<SuperlativeQuestions> allSuperlativeQuestions;

    public SuperlativeQuestionsList()
    {
        allSuperlativeQuestions = new List<SuperlativeQuestions>();
    }
}
