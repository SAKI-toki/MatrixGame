using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 1), Tooltip("定位置に移動する速度")]
    float fixedSpeed = 0.1f;
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
    //定位置にいるかどうか
    bool isFixedPosition = true;
    public bool IsFixedPosition { get { return this.isFixedPosition; } }

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
            //定位置に向かって移動する
            position.x = Mathf.Lerp(position.x, localFixedPositionX, fixedSpeed);
            //定位置ではない
            isFixedPosition = false;
        }
        else
        {
            //定位置である
            isFixedPosition = true;
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