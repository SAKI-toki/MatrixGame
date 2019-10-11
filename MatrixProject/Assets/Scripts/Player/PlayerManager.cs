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
    [SerializeField]
    ScrollController scrollController = null;
    //プレイヤーの数
    static int PlayerCount = 0;
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
            //コントローラーを取得
            players.Add(playerObject.GetComponent<PlayerController>());
            players[i].playerNumber = i;
            scrollController.AddScrollList(players[i]);
        }
        Physics.Simulate(10.0f);
    }

    void Update()
    {
        PlayerSort();
        for (int i = 0; i < players.Count; ++i)
        {
            players[i].MoveUpdate(GetPlayerFixedPositionX(i));
        }
    }

    /// <summary>
    /// プレイヤーのソート
    /// </summary>
    void PlayerSort()
    {
        //どのくらい追い越したらソートするか
        const float OverLength = 0.2f;
        for (int i = 0; i < players.Count - 1; ++i)
        {
            if (players[i].transform.position.x + OverLength <
                players[i + 1].transform.position.x)
            {
                var temp = players[i];
                players[i] = players[i + 1];
                players[i + 1] = temp;
            }
        }
    }

    [SerializeField, Tooltip("カメラの中心のTransform")]
    Transform CenterTransform = null;
    /// <summary>
    /// 中心のX軸の座標を取得
    /// </summary>
    float GetCenterPositionX()
    {
        return CenterTransform.position.x;
    }
    /// <summary>
    /// プレイヤーの定位置のX軸の座標を取得
    /// </summary>
    float GetPlayerFixedPositionX(int index)
    {
        return GetCenterPositionX() + leadPositionX - interval * index;
    }
}
