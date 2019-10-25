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

    void IScrollObject.Scroll(Vector3 scrollValue)
    {
        position = transform.position;
        position += scrollValue * Time.deltaTime;
        transform.position = position;
    }
}