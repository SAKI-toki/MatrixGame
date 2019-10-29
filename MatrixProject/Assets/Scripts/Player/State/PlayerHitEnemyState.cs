using UnityEngine;

public partial class PlayerController
{
    [SerializeField, Tooltip("敵にあった後のクールタイム")]
    float hitEnemyCoolTime = 2.0f;
    [SerializeField, Tooltip("敵に当たった後の点滅する速度")]
    float flashSpeed = 5.0f;

    /// <summary>
    /// プレイヤーが敵にヒットした時のステート
    /// </summary>
    class PlayerHitEnemyState : IPlayerState
    {
        float timeCount = 0.0f;
        Renderer[] renderers = null;

        void IPlayerState.Init(PlayerController playerController)
        {
            //ゴールドを半分にする
            playerController.SetGold(playerController.gold / 2);
            //敵に当たったときのレイヤーに変更
            playerController.gameObject.layer = LayerMask.NameToLayer("PlayerHitEnemy");
            renderers = playerController.GetComponentsInChildren<Renderer>();
        }

        IPlayerState IPlayerState.Update(PlayerController playerController)
        {
            timeCount += Time.deltaTime;

            playerController.Jump();
            playerController.SideMove();
            playerController.SetDefaultVelocity();

            //サインカーブでオンオフを切り替える
            FlashMesh(Mathf.Sin(timeCount * 10 * playerController.flashSpeed) > 0);
            //クールタイム過ぎたらメインステートの戻す
            if (timeCount > playerController.hitEnemyCoolTime)
            {
                return new PlayerMainState();
            }
            return this;
        }

        void IPlayerState.Destroy(PlayerController playerController)
        {
            FlashMesh(true);
            playerController.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        /// <summary>
        /// メッシュの点滅
        /// </summary>
        void FlashMesh(bool on)
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = on;
            }
        }
    }
}