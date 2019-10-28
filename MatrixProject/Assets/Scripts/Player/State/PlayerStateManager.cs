using UnityEngine;

public partial class PlayerController
{
    /// <summary>
    /// プレイヤーのステートのインターフェース
    /// </summary>
    interface IPlayerState
    {
        void Init(PlayerController playerController);
        IPlayerState Update(PlayerController playerController);
        void Destroy(PlayerController playerController);
    }

    /// <summary>
    /// プレイヤーのステートの管理クラス
    /// </summary>
    class PlayerStateManager
    {
        IPlayerState state = null;
        PlayerController playerControllerInstance = null;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(PlayerController playerController, IPlayerState initState)
        {
            playerControllerInstance = playerController;
            SwitchState(initState);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (!IsStateNull()) SwitchState(state.Update(playerControllerInstance));
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Destroy()
        {
            if (!IsStateNull()) state.Destroy(playerControllerInstance);
        }

        /// <summary>
        /// ステートの切り替え
        /// </summary>
        public void SwitchState(IPlayerState nextState)
        {
            if (state != nextState)
            {
                if (!IsStateNull()) state.Destroy(playerControllerInstance);
                state = nextState;
                state.Init(playerControllerInstance);
            }
        }

        /// <summary>
        /// ステートがヌルかどうか
        /// </summary>
        bool IsStateNull()
        {
            return state == null;
        }
    }
}