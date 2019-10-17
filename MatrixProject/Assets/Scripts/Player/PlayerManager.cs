using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public abstract class PlayerManager<PlayerType, AnswerType> : MonoBehaviour where PlayerType : PlayerController<AnswerType>
{
    [SerializeField, Tooltip("先頭のプレイヤーの位置(画面の中心を0とする)")]
    float leadPositionX = 0.0f;
    [SerializeField, Tooltip("プレイヤー同士の間隔")]
    float interval = 1.0f;

    [SerializeField, Tooltip("プレイヤーPrefab")]
    GameObject playerPrefab = null;
    [SerializeField, Tooltip("スクロールオブジェクト")]
    GameObject scrollObject = null;
    //プレイヤーリスト
    protected List<PlayerType> players = new List<PlayerType>();
    static int playerCount = 0;

    protected void Start()
    {
        Debug.Log(sizeof(float) + sizeof(float));
        //#if UNITY_EDITOR
        playerCount = 4;
        //#endif
        //プレイヤーの生成
        for (int i = 0; i < playerCount; ++i)
        {
            //生成
            GameObject playerObject = Instantiate(playerPrefab,
                scrollObject.transform.position + new Vector3(GetPlayerFixedPositionX(i), 0, 0),
                Quaternion.identity);
            //リストに追加
            players.Add(playerObject.AddComponent<PlayerType>());
            //プレイヤーの番号をセット
            players[i].playerNumber = i;
            //スクロールするオブジェクトを親に設定
            players[i].transform.parent = scrollObject.transform;
            players[i].scrollController = scrollObject.GetComponent<ScrollController>();
        }
        InitializePlayerAnswer();
        //プレイヤーを地面につけるために物理演算させる
        Physics.Simulate(10.0f);
    }

    protected void Update()
    {
        for (int i = 0; i < players.Count; ++i)
        {
            players[i].MoveUpdate(GetPlayerFixedPositionX(i));
        }
        //全てのプレイヤーが定位置にいなければ入れ替えができない
        if (IsAllPlayerFixedPosition())
        {
            PlayerExchange();
        }
        if (SwitchInput.GetButton(0, SwitchButton.Stick) && SwitchInput.GetButton(0, SwitchButton.ZTrigger))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }

#if UNITY_EDITOR
    [SerializeField]
    int debugSwitchPlayerNumber = 0;
    [SerializeField]
    bool debugSwitch = false;
#endif
    /// <summary>
    /// プレイヤーを入れ替える
    /// </summary>
    void PlayerExchange()
    {
        //入れ替える
        for (int i = 1; i < players.Count; ++i)
        {
            if (SwitchInput.GetButtonDown(players[i].playerNumber, SwitchButton.Switch)
#if UNITY_EDITOR
            || (debugSwitch && debugSwitchPlayerNumber == i)
#endif
            )
            {
#if UNITY_EDITOR
                debugSwitch = false;
#endif
                var temp = players[0];
                players[0] = players[i];
                players[i] = temp;
                break;
            }
        }
    }

    /// <summary>
    /// プレイヤーの定位置のX軸の座標を取得
    /// </summary>
    float GetPlayerFixedPositionX(int index)
    {
        return leadPositionX - interval * index;
    }

    /// <summary>
    /// 全てのプレイヤーが定位置かどうか
    /// </summary>
    bool IsAllPlayerFixedPosition()
    {
        for (int i = 0; i < players.Count; ++i)
        {
            if (!players[i].IsFixedPosition) return false;
        }
        return true;
    }

    protected abstract void InitializePlayerAnswer();
    public abstract List<AnswerType> GetAnswerList();
}
