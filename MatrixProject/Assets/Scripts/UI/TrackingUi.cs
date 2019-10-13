using UnityEngine;

/// <summary>
/// 指定したオブジェクトを追尾するUI
/// </summary>
public class TrackingUi : MonoBehaviour
{
    [SerializeField, Tooltip("追尾するTransform")]
    Transform targetTransform = null;
    [SerializeField, Tooltip("オフセット")]
    Vector3 offset = Vector3.zero;
    RectTransform rectTransform = null;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        rectTransform.position =
            RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position + offset);
    }
}