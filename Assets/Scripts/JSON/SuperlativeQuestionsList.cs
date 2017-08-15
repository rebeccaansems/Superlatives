using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperlativeQuestionsList
{
    [Serializable]
    public class SQL
    {
        public List<SuperlativeQuestion> Questions;
    }

    public List<SuperlativeQuestion> allSuperlativeQuestions;
    public List<SuperlativeQuestion> rankingQuestions;
    public List<SuperlativeQuestion> mostLikelyQuestions;

    public SuperlativeQuestionsList(string json)
    {
        allSuperlativeQuestions = new List<SuperlativeQuestion>();
        rankingQuestions = new List<SuperlativeQuestion>();
        mostLikelyQuestions = new List<SuperlativeQuestion>();

        LoadAllQuestions(json);
        SplitQuestionsIntoLists();
    }

    private void LoadAllQuestions(string json)
    {
        SQL sq = JsonUtility.FromJson<SQL>(json);
        allSuperlativeQuestions = sq.Questions;
    }

    private void SplitQuestionsIntoLists()
    {
        for (int i = 0; i < allSuperlativeQuestions.Count; i++)
        {
            if (allSuperlativeQuestions[i].isRankingRound)
            {
                rankingQuestions.Add(allSuperlativeQuestions[i]);
            }
            else
            {
                mostLikelyQuestions.Add(allSuperlativeQuestions[i]);
            }
        }
    }
}
