using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーの人数
/// </summary>
static class PlayerNumber
{
    static public int count = 2;
    public const int MaxCount = 4;
}

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("デバッグ用のプレイヤーの人数")]
    int debugPlayerNum = 4;

    [SerializeField, Tooltip("スクロール")]
    ScrollController scrollController = null;
    [SerializeField, Tooltip("プレイヤーリスト")]
    List<GameObject> playerObjects = new List<GameObject>();

    void Awake()
    {
#if UNITY_EDITOR
        PlayerNumber.count = debugPlayerNum;
#endif
    }

    void Start()
    {
        for (int i = 0; i < PlayerNumber.count; ++i)
        {
            //リストに追加
            scrollController.AddList(playerObjects[i].GetComponent<PlayerController>());
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
