﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのUIの管理
/// </summary>
public class PlayerUIManager : MonoBehaviour
{
    [SerializeField, Range(0.125f, 0.15f)]
    float playerUIWidth = 0.15f;

    [SerializeField]
    GameObject playerUIPlafab = null;
    Text[] goldTextArray;

    void Awake()
    {
        PlayerUIGenerator(PlayerNumber.count);
    }

    void Update()
    {
    }

    /// <summary>
    /// UIの生成
    /// </summary>
    void PlayerUIGenerator(int playerNum)
    {
        //playerUIArray = new GameObject[playerNum];
        goldTextArray = new Text[playerNum];
        float interval = (1 - (PlayerNumber.MaxCount * playerUIWidth)) / (PlayerNumber.MaxCount - 1);
        float anchorNum = 0.0f;

        for (int i = 0; i < playerNum; ++i)
        {
            //UIの生成
            GameObject playerUI = Instantiate(playerUIPlafab, transform.position, Quaternion.identity);
            //親をセット
            playerUI.transform.SetParent(transform);
            //名前をセット
            playerUI.name = "PlayerUI" + (i + 1).ToString();
            //playerUIArray[i] = playerUI;
            goldTextArray[i] = FindChildText(playerUI.transform, "GoldText");
            RectTransform playerUIRectTransform = playerUI.GetComponent<RectTransform>();

            //Transformの初期化
            playerUIRectTransform.localPosition = Vector3.zero;
            playerUIRectTransform.sizeDelta = Vector2.zero;
            playerUIRectTransform.localScale = Vector3.one;
            //アンカーのセット
            playerUIRectTransform.anchorMin = new Vector2(anchorNum, 0);
            anchorNum += playerUIWidth;
            playerUIRectTransform.anchorMax = new Vector2(anchorNum, 1);
            anchorNum += interval;
        }
    }

    /// <summary>
    /// ゴールドのテキストを更新
    /// </summary>
    public void GoldUpdate(int index, int n)
    {
        goldTextArray[index].text = n.ToString();
    }

    /// <summary>
    /// Textコンポーネントのある子要素を探す
    /// </summary>
    Text FindChildText(Transform parent, string name)
    {
        var childTextComponents = parent.GetComponentsInChildren<Text>();
        foreach (var textComponent in childTextComponents)
        {
            if (textComponent.name == name)
                return textComponent;
        }
        return null;
    }
}
