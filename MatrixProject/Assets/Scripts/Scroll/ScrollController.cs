using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// スクロールを制御するクラス
/// </summary>
public class ScrollController : MonoBehaviour
{
    [SerializeField, Tooltip("通常のスクロールスピード")]
    float defaultScrollSpeed = 1.0f;

    float scrollScale = 1.0f;

    Vector3 position;

    void LateUpdate()
    {
        position = transform.position;
        position.x += defaultScrollSpeed * scrollScale * Time.deltaTime;
        transform.position = position;
    }
}
