using UnityEngine;

/// <summary>
/// スクロールを制御するクラス
/// </summary>
public class ScrollController : MonoBehaviour
{
    [SerializeField, Tooltip("通常のスクロールスピード")]
    float defaultScrollSpeed = 1.0f;

    float scrollScale = 1.0f;

    Vector3 position;

    float knockBackPower = 0.0f;
    float knockBackTime = 0.0f;
    bool knockBackFlg = false;

    void Update()
    {
        if (knockBackFlg)
        {
            knockBackTime += Time.deltaTime;
            scrollScale = Mathf.Lerp(knockBackPower, 1.0f, knockBackTime);
            if (knockBackTime >= 1.0f) knockBackFlg = false;
        }
        position = transform.position;
        position.x += defaultScrollSpeed * scrollScale * Time.deltaTime;
        transform.position = position;
    }

    /// <summary>
    /// ノックバックする関数
    /// </summary>
    public void KnockBack(float power)
    {
        knockBackPower = -power;
        knockBackTime = 0.0f;
        knockBackFlg = true;
    }
}