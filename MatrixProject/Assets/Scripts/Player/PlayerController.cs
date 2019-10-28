using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public partial class PlayerController : MonoBehaviour, IScrollObject
{
    //ステートの管理クラス
    PlayerStateManager stateManager = new PlayerStateManager();

    [SerializeField, Tooltip("プレイヤーの番号"), Range(0, 3)]
    int playerNumber = 0;

    [SerializeField, Tooltip("横移動の力")]
    float sideMoveSpeed = 10.0f;
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpPower = 10.0f;
    [SerializeField, Tooltip("重力")]
    float gravityPower = 15.0f;
    [SerializeField, Tooltip("移動範囲")]
    float moveRange = 9.0f;
    [SerializeField, Tooltip("リジッドボディ")]
    new Rigidbody rigidbody = null;
    [SerializeField, Tooltip("UIの管理クラス")]
    PlayerUIManager uiManager = null;
    [SerializeField, Tooltip("アニメーションの制御")]
    PlayerAnimationController animationController = null;

    //x方向の力
    float velocityX = 0.0f;

    //所持ゴールド
    int gold;

    void Start()
    {
        SetGold(0);
        stateManager.Init(this, new PlayerMainState());
    }

    void Update()
    {
        stateManager.Update();
        //常に下に重力をPhysicsのデフォルトとは別に追加する
        rigidbody.AddForce(Vector3.up * -gravityPower);

        velocityX = 0.0f;
    }

    void OnDestroy()
    {
        stateManager.Destroy();
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        //ジャンプボタンを押し、地面についているときにジャンプする
        if (SwitchInput.GetButtonDown(playerNumber, SwitchButton.Jump) && IsGround())
        {
            var velocity = rigidbody.velocity;
            velocity.y = 0.0f;
            rigidbody.velocity = velocity;
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
        if (l == r) return;
        //減速
        if (l)
        {
            velocityX -= sideMoveSpeed;
        }
        //加速
        else //if(r)
        {
            velocityX += sideMoveSpeed;
        }
    }

    /// <summary>
    /// 力のセット
    /// </summary>
    void SetVelocity()
    {
        var velocity = rigidbody.velocity;
        velocity.x = velocityX;
        rigidbody.velocity = velocity;

        animationController.SetSpeed(velocityX);
    }

    /// <summary>
    /// 範囲内に収める
    /// </summary>
    void FitRange()
    {
        if (transform.position.x > moveRange)
        {
            var position = transform.position;
            position.x = moveRange;
            transform.position = position;
        }
    }

    /// <summary>
    /// コインの取得
    /// </summary>
    public void IncrementGold(int addGold)
    {
        SetGold(gold + addGold);
    }

    void OnCollisionEnter(Collision other)
    {
        //敵と衝突
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            stateManager.SwitchState(new PlayerHitEnemyState());
            //Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //アイテムと衝突
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            other.gameObject.GetComponent<ItemBase>().GetItemFunction(this);
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// スクロール
    /// </summary>
    void IScrollObject.Scroll(Vector3 scrollValue)
    {
        velocityX += scrollValue.x;
        moveRange += scrollValue.x * Time.deltaTime;
    }

    /// <summary>
    /// コインの数のセット
    /// </summary>
    void SetGold(int n)
    {
        gold = n;
        uiManager.SetGold(playerNumber, gold);
    }

    /// <summary>
    /// プレイヤーの番号をセット
    /// </summary>
    public void SetPlayerNumber(int x)
    {
        playerNumber = x;
    }

    /// <summary>
    /// 所持ゴールドの取得
    /// </summary>
    public int GetGold()
    {
        return gold;
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
}