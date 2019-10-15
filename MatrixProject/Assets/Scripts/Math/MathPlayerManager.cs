using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 算数用のプレイヤー管理クラス
/// </summary>
public class MathPlayerManager : PlayerManager<MathPlayerController, int>
{
    /// <summary>
    /// 答えリストの取得
    /// </summary>
    public override List<int> GetAnswerList()
    {
        List<int> answerList = new List<int>();
        foreach (var player in players)
        {
            answerList.Add(player.answer);
        }
        return answerList;
    }
}