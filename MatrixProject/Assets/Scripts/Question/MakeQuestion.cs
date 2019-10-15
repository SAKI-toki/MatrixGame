using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 問題を作成する
/// </summary>
public abstract class MakeQuestion<T> : MonoBehaviour
{
    /// <summary>
    /// 問題を作成
    /// </summary>
    public abstract Question<T> Make(List<T> answerList);
}