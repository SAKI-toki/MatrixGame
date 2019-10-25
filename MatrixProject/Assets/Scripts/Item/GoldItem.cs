using UnityEngine;

/// <summary>
/// ゴールドアイテム
/// </summary>
public class GoldItem : ItemBase
{
    [SerializeField, Tooltip("取得するゴールド")]
    int gold = 1;

    public override void GetItemFunction(PlayerController playerController)
    {
        playerController.IncrementGold(gold);
    }
}