using UnityEngine;

public class ScrollObject : MonoBehaviour, IScrollObject
{
    void IScrollObject.Scroll(float scrollValueX)
    {
        var pos = transform.position;
        pos.x += scrollValueX * Time.deltaTime;
        transform.position = pos;
    }
}