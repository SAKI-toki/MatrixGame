using UnityEngine;

/// <summary>
/// 算数用のプレイヤー制御クラス
/// </summary>
public class MathPlayerController : PlayerController<int>
{
#if UNITY_EDITOR
    new void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            answer = Random.Range(0, 10);
        }
    }
#endif
}