using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuperlativeQuestions
{
    public string superlativeQuestion;
    public bool isRankingRound;

    public SuperlativeQuestions(string superlativeQuestion, bool isInt, bool isRankingRound)
    {
        this.superlativeQuestion = superlativeQuestion;
        this.isRankingRound = isRankingRound;
    }
}
