﻿using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IScrollObject
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
    [SerializeField, Tooltip("移動範囲")]
    float moveRange = 9.0f;

    float velocityX = 0.0f;

    void Update()
    {
        if (transform.position.x > moveRange)
        {
            var position = transform.position;
            position.x = moveRange;
            transform.position = position;
        }
        velocityX = 0.0f;
        //常に下に重力をPhysicsのデフォルトとは別に追加する
        rigidbody.AddForce(Vector3.up * -gravityPower);
        Jump();
        SideMove();
    }

    void LateUpdate()
    {
        var velocity = rigidbody.velocity;
        velocity.x = velocityX;
        rigidbody.velocity = velocity;
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

    void IScrollObject.Scroll(float scrollValue)
    {
        velocityX += scrollValue;
        moveRange += scrollValue * Time.deltaTime;
    }
}