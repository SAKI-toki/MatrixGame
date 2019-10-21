using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //プレイヤーの番号(0~3)
    int playerNumber = 0;

    [SerializeField, Tooltip("横移動の力")]
    float sideMoveSpeed = 10.0f;
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpPower = 10.0f;
    [SerializeField, Tooltip("重力")]
    float gravityPower = 15.0f;
    [SerializeField, Tooltip("リジッドボディ")]
    new Rigidbody rigidbody = null;

    void Update()
    {
        //常に下に重力をPhysicsのデフォルトとは別に追加する
        rigidbody.AddForce(Vector3.up * -gravityPower);
        Jump();
        SideMove();
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        //ジャンプボタンを押し、y方向の力が0に限りなく近く、地面についているときにジャンプする
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
        if (l != r)
        {
            //減速
            if (l)
            {
                position.x -= sideMoveSpeed * Time.deltaTime;
            }
            //加速
            else //if(r)
            {
                position.x += sideMoveSpeed * Time.deltaTime;
            }
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

    /// <summary>
    /// 敵に当たったときの処理
    /// </summary>
    void HitEnemy()
    {
    }

    /// <summary>
    /// プレイヤーの番号をセット
    /// </summary>
    public void SetPlayerNumber(int x)
    {
        playerNumber = x;
    }

    void OnCollisionEnter(Collision other)
    {
        //敵と衝突
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy();
        }
    }
}