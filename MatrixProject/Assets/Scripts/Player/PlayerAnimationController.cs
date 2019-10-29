using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションの制御
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーター")]
    Animator animator = null;

    /// <summary>
    /// 速度をセットする
    /// </summary>
    public void SetSpeed(float speed)
    {
        animator.speed = speed;
    }
}
