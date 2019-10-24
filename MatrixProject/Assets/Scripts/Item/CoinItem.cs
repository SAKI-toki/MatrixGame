using UnityEngine;

/// <summary>
/// コイン
/// </summary>
public class CoinItem : ItemBase
{
    public override void GetItemFunction(PlayerController playerController)
    {
        playerController.GetCoin();
    }
}