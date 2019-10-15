using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の管理クラス
/// </summary>
public abstract class EnemyManager<EnemyType, AnswerType> : MonoBehaviour where EnemyType : EnemyController<AnswerType>
{
    List<EnemyType> enemyList = new List<EnemyType>();

    //画面中央からどのくらい近づいたら問題を生成するか(X軸)
    const float makeQuestionRangeX = 15.0f;
    [SerializeField, Tooltip("敵が入っている親オブジェクトのTransform")]
    Transform enemyTransform = null;
    [SerializeField, Tooltip("スクロールオブジェクトのTransform")]
    Transform scrollTransform = null;

    protected void Start()
    {
        for (int i = 0; i < enemyTransform.childCount; ++i)
        {
            var enemy = enemyTransform.GetChild(i).gameObject.AddComponent<EnemyType>();
            EnemyInitialize(enemy);
            enemyList.Add(enemy);
        }
    }

    protected void Update()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy.alwaysSetQuestion) continue;
            //近くに来たら問題をセットする
            if (enemy.transform.position.x < scrollTransform.position.x + makeQuestionRangeX)
            {
                enemy.SetQuestion(GetAnswerList());
            }
        }
    }

    protected abstract List<AnswerType> GetAnswerList();
    protected abstract void EnemyInitialize(EnemyType enemy);

}