using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class PlayerController<AnswerType> : MonoBehaviour
{
    new Rigidbody rigidbody;

    //プレイヤーの番号(0~3)
    [System.NonSerialized]
    public int playerNumber = 0;
    //定位置にいるかどうか
    bool isFixedPosition = true;
    public bool IsFixedPosition { get { return isFixedPosition; } }
    //自分の答えを保持する
    public AnswerType answer = default(AnswerType);
    Text answerText = null;
    PlayerConfig playerConfigInstance = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerConfigInstance = PlayerConfig.GetInstance();
        answerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        rigidbody.AddForce(Vector3.up * -playerConfigInstance.GravityPower);
        answerText.text = answer.ToString();
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
            position.x = Mathf.Lerp(position.x, localFixedPositionX, playerConfigInstance.FixedSpeed);
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
            rigidbody.AddForce(Vector3.up * playerConfigInstance.JumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 疑似的に地面についているかどうか返す
    /// </summary>
    bool IsGround()
    {
        const float rayLength = 0.3f;
        Vector3 rayPosition = transform.position - new Vector3(0, 0.8f, 0);
        return Physics.Linecast(rayPosition, rayPosition - Vector3.up * rayLength);
    }

    private void OnCollisionEnter(Collision other)
    {
        //敵と衝突
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemyAnswer = other.transform.GetComponent<EnemyController<AnswerType>>().GetAnswer();
            if (answer.Equals(enemyAnswer))
            {
                Debug.Log("==");
            }
            else
            {
                Debug.Log("!=");
            }
            Destroy(other.gameObject);
        }
    }
}