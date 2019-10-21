using UnityEngine;

public class ScrollObject : MonoBehaviour, IScrollObject
{
    [SerializeField]
    ScrollController scrollController = null;

    void Start()
    {
        scrollController.AddList(this);
    }

    Vector3 position = Vector3.zero;

    void IScrollObject.Scroll(float scrollValue)
    {
        position = transform.position;
        position.x += scrollValue * Time.deltaTime;
        transform.position = position;
    }
}