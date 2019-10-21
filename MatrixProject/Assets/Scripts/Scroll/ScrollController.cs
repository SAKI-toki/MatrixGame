using UnityEngine;
using System.Collections.Generic;

public interface IScrollObject
{
    void Scroll(float scrollValue);
}

/// <summary>
/// スクロールを制御するクラス
/// </summary>
public class ScrollController : MonoBehaviour
{
    [SerializeField, Tooltip("通常のスクロールスピード")]
    float defaultScrollSpeed = 1.0f;

    float scrollScale = 1.0f;

    Vector3 position;

    List<IScrollObject> scrollObjects = new List<IScrollObject>();

    void Update()
    {
        for (int i = scrollObjects.Count - 1; i >= 0; --i)
        {
            if (scrollObjects[i] == null)
            {
                scrollObjects.RemoveAt(i);
            }
            else
            {
                scrollObjects[i].Scroll(defaultScrollSpeed * scrollScale);
            }
        }
    }

    public void AddList(IScrollObject addObject)
    {
        scrollObjects.Add(addObject);
    }
}
