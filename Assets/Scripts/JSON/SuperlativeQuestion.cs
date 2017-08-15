using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuperlativeQuestion
{
    public string superlativeQuestion;
    public bool isRankingRound;

    public SuperlativeQuestion(string superlativeQuestion, bool isInt, bool isRankingRound)
    {
        this.superlativeQuestion = superlativeQuestion;
        this.isRankingRound = isRankingRound;
    }
}
