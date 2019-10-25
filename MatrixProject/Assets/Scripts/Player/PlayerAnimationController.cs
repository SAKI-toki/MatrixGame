using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーター")]
    Animator animator = null;

    /// <summary>
    /// 速度をセットする
    /// </summary>
    public void SetSpeed(float speed)
    {
        var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.speed = speed;
    }
}
