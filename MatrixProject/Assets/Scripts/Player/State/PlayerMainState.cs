using UnityEngine;

public partial class PlayerController
{
    /// <summary>
    /// プレイヤーのメインステート
    /// </summary>
    class PlayerMainState : IPlayerState
    {
        void IPlayerState.Init(PlayerController playerController)
        {
        }

        IPlayerState IPlayerState.Update(PlayerController playerController)
        {
            playerController.FitRange();
            playerController.Jump();
            playerController.SideMove();
            playerController.SetDefaultVelocity();
            return this;
        }

        void IPlayerState.Destroy(PlayerController playerController)
        {
        }
    }
}