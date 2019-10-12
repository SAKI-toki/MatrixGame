using UnityEngine;

/// <summary>
/// プレイヤーを一括でスクロールさせる
/// </summary>
public class PlayerScroll : MonoBehaviour, IScrollObject
{
    void IScrollObject.Scroll(float scrollValueX)
    {
        var position = transform.position;
        position.x += scrollValueX * Time.deltaTime;
        transform.position = position;
    }
}