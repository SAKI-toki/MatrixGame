using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 敵の制御
/// </summary>
public abstract class EnemyController<AnswerType> : MonoBehaviour
{
    Question<AnswerType> question = null;
    Text questionUI = null;
    [System.NonSerialized]
    public bool alwaysSetQuestion = false;

    protected void Start()
    {
        questionUI = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// 問題をセット
    /// </summary>
    public void SetQuestion(List<AnswerType> answerList)
    {
        if (alwaysSetQuestion) return;
        alwaysSetQuestion = true;
        question = MakeQuestion(answerList);
        questionUI.text = question.QuestionString;
    }

    /// <summary>
    /// 答えの取得
    /// </summary>
    public AnswerType GetAnswer()
    {
        return question.Answer;
    }

    protected abstract Question<AnswerType> MakeQuestion(List<AnswerType> answerList);
}
