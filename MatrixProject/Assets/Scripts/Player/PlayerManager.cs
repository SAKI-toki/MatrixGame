using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーの人数
/// </summary>
static class PlayerNumber
{
    static public int count = 0;
    public const int MaxCount = 4;
}

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("スクロール")]
    ScrollController scrollController = null;
    [SerializeField, Tooltip("プレイヤーリスト")]
    List<GameObject> playerObjects = new List<GameObject>();

    void Start()
    {
        //#if UNITY_EDITOR
        PlayerNumber.count = 4;
        //#endif
        for (int i = 0; i < PlayerNumber.count; ++i)
        {
            //プレイヤーの番号をセット
            var playerController = playerObjects[i].GetComponent<PlayerController>();
            playerController.SetPlayerNumber(i);
            //リストに追加
            scrollController.AddList(playerController);
        }
        for (int i = PlayerNumber.count; i < PlayerNumber.MaxCount; ++i)
        {
            Destroy(playerObjects[i]);
        }
        playerObjects.RemoveRange(PlayerNumber.count, PlayerNumber.MaxCount - PlayerNumber.count);
        //プレイヤーを地面につけるために物理演算させる
        Physics.Simulate(10.0f);
    }
}
