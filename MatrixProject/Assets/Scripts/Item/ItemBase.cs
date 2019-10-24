using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class ItemBase : MonoBehaviour
{
    public abstract void GetItemFunction(PlayerController playerController);
}
