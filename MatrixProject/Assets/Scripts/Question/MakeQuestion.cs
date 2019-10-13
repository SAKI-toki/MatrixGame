using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 問題を作成する
/// </summary>
public abstract class MakeQuestion<T> : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    List<T> debugList = new List<T>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var q = Make(debugList);
            Debug.Log(q.Answer);
            Debug.Log(q.QuestionString);
        }
    }
#endif
    /// <summary>
    /// 問題を作成
    /// </summary>
    public abstract Question<T> Make(List<T> numberList);
}