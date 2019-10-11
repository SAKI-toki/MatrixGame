using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IScrollObject
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
    public void MoveUpdate(float fixedPositionX)
    {
        FixedPositionMove(fixedPositionX);
        Jump();
    }

    /// <summary>
    /// 定位置に寄せる
    /// </summary>
    void FixedPositionMove(float fixedPositionX)
    {
        var position = rigidbody.position;
        //ある程度近くないと加速、減速する
        if (Mathf.Abs(position.x - fixedPositionX) > 0.1f)
        {
            position.x = Mathf.Lerp(position.x, fixedPositionX, 0.03f);
        }
        rigidbody.MovePosition(position);
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

    void IScrollObject.Scroll(float scrollValueX)
    {
        rigidbody.MovePosition(rigidbody.position + Vector3.right * scrollValueX * Time.deltaTime);
    }

}