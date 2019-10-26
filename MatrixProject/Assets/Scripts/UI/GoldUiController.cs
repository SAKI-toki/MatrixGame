using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゴールドを表示するUIの制御
/// </summary>
public class GoldUiController : MonoBehaviour
{
    [SerializeField, Tooltip("ゴールドを表示するUI")]
    Text goldText = null;

    /// <summary>
    /// 所持ゴールドのセット
    /// </summary>
    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}