using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーの人数
/// </summary>
static class PlayerNumber
{
    static public int count = 0;
}

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("スクロール")]
    ScrollController scrollController = null;
    [SerializeField, Tooltip("プレイヤーPrefab")]
    GameObject playerPrefab = null;
    [SerializeField, Tooltip("プレイヤーの初期位置")]
    List<Transform> playerInitTransform = new List<Transform>();
    //プレイヤーリスト
    List<PlayerController> players = new List<PlayerController>();

    void Start()
    {
        //#if UNITY_EDITOR
        PlayerNumber.count = 4;
        //#endif
        for (int i = 0; i < PlayerNumber.count; ++i)
        {
            //生成
            GameObject playerObject = Instantiate(playerPrefab,
                playerInitTransform[i].position, playerInitTransform[i].rotation);
            //リストに追加
            players.Add(playerObject.GetComponent<PlayerController>());
            //プレイヤーの番号をセット
            players[i].SetPlayerNumber(i);
            //リストに追加
            scrollController.AddList(players[i]);
        }
        //プレイヤーを地面につけるために物理演算させる
        Physics.Simulate(10.0f);
    }
}
