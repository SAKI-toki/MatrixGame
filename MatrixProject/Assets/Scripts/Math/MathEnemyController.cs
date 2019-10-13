using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 算数の敵の制御クラス
/// </summary>
public class MathEnemyController : EnemyController<int>
{
    MakeMathQuestion makeMathQuestion = null;

    public void SetMakeMathQuestion(MakeMathQuestion instance)
    {
        makeMathQuestion = instance;
    }

    protected override Question<int> MakeQuestion(List<int> answerList)
    {
        return makeMathQuestion.Make(answerList);
    }
}