using UnityEngine;
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

    UiController[] uiControllers = null;

    //既に初期化したかどうか
    bool alreadyInitialize = false;


    void Start()
    {
        Initialize();
    }

    void Update()
    {
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize()
    {
        if (!alreadyInitialize) PlayerUIGenerator(PlayerNumber.count);
    }

    /// <summary>
    /// UIの生成
    /// </summary>
    void PlayerUIGenerator(int playerNum)
    {
        //初期化したのでtrueにする
        alreadyInitialize = true;
        //配列の要素数の確保
        uiControllers = new UiController[playerNum];
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
            uiControllers[i] = playerUI.GetComponent<UiController>();

            //Transformの初期化
            RectTransform playerUIRectTransform = playerUI.GetComponent<RectTransform>();
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
    public void SetGold(int index, int n)
    {
        if (!alreadyInitialize) PlayerUIGenerator(PlayerNumber.count);
        uiControllers[index].SetGold(n);
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
