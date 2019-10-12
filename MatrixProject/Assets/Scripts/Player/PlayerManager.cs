using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("先頭のプレイヤーの位置(X)")]
    float leadPositionX = 0.0f;
    [SerializeField, Tooltip("プレイヤー同士の間隔")]
    float interval = 1.0f;

    [SerializeField, Tooltip("プレイヤーPrefab")]
    GameObject playerPrefab = null;
    [SerializeField, Tooltip("スクロールオブジェクト")]
    GameObject scrollObject = null;
    //プレイヤーの数
    static int PlayerCount = 0;
    //プレイヤーリスト
    List<PlayerController> players = new List<PlayerController>(PlayerCount);

    void Start()
    {
#if true
        PlayerCount = 4;
#endif
        //プレイヤーの生成
        for (int i = 0; i < PlayerCount; ++i)
        {
            //生成
            GameObject playerObject = Instantiate(playerPrefab,
                new Vector3(GetPlayerFixedPositionX(i), 0, 0),
                Quaternion.identity);
            //リストに追加
            players.Add(playerObject.GetComponent<PlayerController>());
            //プレイヤーの番号をセット
            players[i].playerNumber = i;
            //スクロールするオブジェクトを親に設定
            players[i].transform.parent = scrollObject.transform;
        }
        //プレイヤーを地面につけるために物理演算させる
        Physics.Simulate(10.0f);
    }

#if UNITY_EDITOR
    [SerializeField]
    int debugSwitchPlayerNumber = 0;
    [SerializeField]
    bool debugSwitch = false;
#endif

    void Update()
    {
        for (int i = 0; i < players.Count; ++i)
        {
            players[i].MoveUpdate(GetPlayerFixedPositionX(i));
        }
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
}
