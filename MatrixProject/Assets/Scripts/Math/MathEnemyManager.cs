using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 算数の敵の管理クラス
/// </summary>
public class MathEnemyManager : EnemyManager<MathEnemyController, int>
{
    [SerializeField, Tooltip("プレイヤーの管理クラス")]
    MathPlayerManager playerManager = null;
    [SerializeField, Tooltip("算数の問題作成クラス")]
    MakeMathQuestion makeMathQuestion = null;

    protected override List<int> GetAnswerList()
    {
        return playerManager.GetAnswerList();
    }

    protected override void EnemyInitialize(MathEnemyController mathEnemyController)
    {
        mathEnemyController.SetMakeMathQuestion(makeMathQuestion);
    }
}