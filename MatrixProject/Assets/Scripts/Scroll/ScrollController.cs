using UnityEngine;
using System.Collections.Generic;

public interface IScrollObject
{
    void Scroll(Vector3 scrollValue);
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

    //スクロールの向き(正規化は必ずする)
    Vector3 scrollDirection = new Vector3(1, 0, 0);

    List<IScrollObject> scrollObjects = new List<IScrollObject>();

    void Update()
    {
        var passScroll = scrollDirection * defaultScrollSpeed * scrollScale;
        for (int i = scrollObjects.Count - 1; i >= 0; --i)
        {
            if (scrollObjects[i] == null)
            {
                scrollObjects.RemoveAt(i);
            }
            else
            {
                scrollObjects[i].Scroll(passScroll);
            }
        }
    }

    public void AddList(IScrollObject addObject)
    {
        scrollObjects.Add(addObject);
    }
}
