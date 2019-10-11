using UnityEngine;

/// <summary>
/// カメラのスクロール
/// </summary>
public class CameraScroll : MonoBehaviour, IScrollObject
{
    [SerializeField, Tooltip("スクロールを制御するクラス")]
    ScrollController scrollController = null;
    void Start()
    {
        scrollController.AddScrollList(this);
    }

    void IScrollObject.Scroll(float scrollValueX)
    {
        transform.position = transform.position + Vector3.right * scrollValueX * Time.deltaTime;
    }
}
