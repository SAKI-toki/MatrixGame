using UnityEngine;

/// <summary>
/// UIの制御
/// </summary>
public class UiController : MonoBehaviour
{
    [SerializeField, Tooltip("所持ゴールドを表示するUIの制御")]
    GoldUiController goldUiController = null;

    /// <summary>
    /// ゴールドのセット
    /// </summary>
    public void SetGold(int gold)
    {
        goldUiController.SetGold(gold);
    }
}