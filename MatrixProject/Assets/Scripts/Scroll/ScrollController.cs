using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// スクロールさせるオブジェクトのインターフェース
/// </summary>
public interface IScrollObject
{
    void Scroll(float scrollValueX);
}

/// <summary>
/// スクロールを制御するクラス
/// </summary>
public class ScrollController : MonoBehaviour
{
    [SerializeField, Tooltip("通常のスクロールスピード")]
    float defaultScrollSpeed = 1.0f;
    List<IScrollObject> scrollObjectList = new List<IScrollObject>();

    void Update()
    {
        foreach (var scrollObject in scrollObjectList)
        {
            if (scrollObject == null)
            {
                scrollObjectList.Remove(scrollObject);
                continue;
            }
            scrollObject.Scroll(defaultScrollSpeed);
        }
    }

    /// <summary>
    /// スクロールするリストに追加する
    /// </summary>
    public void AddScrollList(IScrollObject scrollObject)
    {
        scrollObjectList.Add(scrollObject);
    }
}