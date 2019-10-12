using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpPower = 10.0f;
    [SerializeField, Tooltip("重力")]
    float gravityPower = 15.0f;

    [SerializeField, Tooltip("地面に向けたレイを飛ばすポイント")]
    Transform groundRayPoint = null;
    [SerializeField, Tooltip("地面に向けたレイの長さ")]
    float groundRayLength = 1.0f;
    new Rigidbody rigidbody;

    //プレイヤーの番号(0~3)
    [System.NonSerialized]
    public int playerNumber = 0;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.AddForce(Vector3.up * -gravityPower);
    }

    /// <summary>
    /// 移動時の更新
    /// </summary>
    public void MoveUpdate(float localFixedPositionX)
    {
        FixedPositionMove(localFixedPositionX);
        Jump();
    }

    /// <summary>
    /// 定位置に寄せる
    /// </summary>
    void FixedPositionMove(float localFixedPositionX)
    {
        var position = transform.localPosition;
        //ある程度近くないと加速、減速する
        if (Mathf.Abs(position.x - localFixedPositionX) > 0.1f)
        {
            position.x = Mathf.Lerp(position.x, localFixedPositionX, 0.06f);
        }
        transform.localPosition = position;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        if (SwitchInput.GetButtonDown(playerNumber, SwitchButton.Jump) && IsGround())
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 疑似的に地面についているかどうか返す
    /// </summary>
    bool IsGround()
    {
        return Physics.Linecast(groundRayPoint.position, groundRayPoint.position - Vector3.up * groundRayLength);
    }

}