using UnityEngine;

/// <summary>
/// プレイヤーの設定
/// </summary>
public class PlayerConfig : Singleton<PlayerConfig>
{
    [SerializeField, Range(0, 1), Tooltip("定位置に移動する速度")]
    float fixedSpeed = 0.1f;
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpPower = 10.0f;
    [SerializeField, Tooltip("重力")]
    float gravityPower = 15.0f;

    public float FixedSpeed { get { return fixedSpeed; } }
    public float JumpPower { get { return jumpPower; } }
    public float GravityPower { get { return gravityPower; } }
}