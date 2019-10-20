using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("横移動の力")]
    float sideMovePower = 10.0f;
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpPower = 10.0f;
    [SerializeField, Tooltip("重力")]
    float gravityPower = 15.0f;

    new Rigidbody rigidbody;

    //プレイヤーの番号(0~3)
    [System.NonSerialized]
    public int playerNumber = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.AddForce(Vector3.up * -gravityPower);
        Jump();
        SideMove();
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        if (SwitchInput.GetButtonDown(playerNumber, SwitchButton.Jump) &&
        Mathf.Abs(rigidbody.velocity.y) < 0.001f && IsGround())
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 横の移動
    /// </summary>
    void SideMove()
    {
        bool l = SwitchInput.GetButton(playerNumber, SwitchButton.SL);
        bool r = SwitchInput.GetButton(playerNumber, SwitchButton.SR);
        Vector3 position = transform.position;
        //どちらも押していない、またはどちらも押している場合は移動しない
        if (l == r)
        {
            return;
        }
        //減速
        else if (l)
        {
            position.x -= sideMovePower * Time.deltaTime;
        }
        //加速
        else if (r)
        {
            position.x += sideMovePower * Time.deltaTime;
        }
        transform.position = position;
    }

    /// <summary>
    /// 疑似的に地面についているかどうか返す
    /// </summary>
    bool IsGround()
    {
        const float rayLength = 0.15f;
        Vector3 rayPosition = transform.position - new Vector3(0, 0.9f, 0);
        return Physics.Linecast(rayPosition, rayPosition - Vector3.up * rayLength);
    }

    private void OnCollisionEnter(Collision other)
    {
        //敵と衝突
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
        }
    }
}