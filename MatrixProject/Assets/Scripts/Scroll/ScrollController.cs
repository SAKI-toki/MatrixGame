using UnityEngine;

/// <summary>
/// スクロールを制御するクラス
/// </summary>
public class ScrollController : MonoBehaviour
{
    [SerializeField, Tooltip("通常のスクロールスピード")]
    float defaultScrollSpeed = 1.0f;

    public float scrollScale = 1.0f;

    Vector3 position;

    void Update()
    {
        position = transform.position;
        position.x += defaultScrollSpeed * scrollScale * Time.deltaTime;
        transform.position = position;
    }
}