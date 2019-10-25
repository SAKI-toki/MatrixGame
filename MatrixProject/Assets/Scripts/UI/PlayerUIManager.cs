using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    RectTransform canvasRectTransform = null;

    [SerializeField, Range(1, 4)]
    int testPlayerNum = 4;
    int maxPlayerNum = 4;

    [SerializeField, Range(0.125f, 0.15f)]
    float playerUIWidth = 0.15f;

    [SerializeField]
    GameObject playerUIPlafab = null;

    GameObject[] playerUIArray;
    Text[] goldTextArray;

    void Start()
    {
        PlayerUIGenerator(testPlayerNum);
    }

    void Update()
    {
    }

    /// <summary>
    /// UIの生成
    /// </summary>
    void PlayerUIGenerator(int playerNum)
    {
        playerUIArray = new GameObject[playerNum];
        goldTextArray = new Text[playerNum];
        float interval = (1 - (maxPlayerNum * playerUIWidth)) / (maxPlayerNum - 1);
        float anchorNum = 0.0f;

        for (int i = 0; i < playerNum; ++i)
        {
            GameObject playerUI = Instantiate(playerUIPlafab, transform.position, Quaternion.identity);
            playerUI.transform.SetParent(transform);
            playerUI.name = "PlayerUI" + (i + 1).ToString();
            playerUIArray[i] = playerUI;
            goldTextArray[i] = FindChildText(playerUI.transform, "GoldText");
            RectTransform playerUIRectTransform = playerUI.GetComponent<RectTransform>();

            playerUIRectTransform.localPosition = Vector3.zero;
            playerUIRectTransform.sizeDelta = Vector2.zero;
            playerUIRectTransform.localScale = Vector3.one;

            playerUIRectTransform.anchorMin = new Vector2(anchorNum, 0);
            anchorNum += playerUIWidth;
            playerUIRectTransform.anchorMax = new Vector2(anchorNum, 1);
            anchorNum += interval;
        }
    }

    /// <summary>
    /// ゴールドのテキストを更新
    /// </summary>
    void GoldUpdate(int index,int n)
    {
        goldTextArray[index].text = n.ToString();
    }

    /// <summary>
    /// Textコンポーネントのある子要素を探す
    /// </summary>
    Text FindChildText(Transform parent,string name)
    {
        foreach(Transform child in parent)
        {
            if (null != child.GetComponent<Text>())
            {
                if (child.name == name)
                {
                    return child.GetComponent<Text>();
                }
            }
        }
        return null;
    }
}
