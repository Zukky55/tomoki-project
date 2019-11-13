using UnityEngine;
using System.Collections;
using System;
using UniRx.Async;

namespace VRShooting
{
    public class GameClear : StateBehaviour
    {
        public event Action OnGameClearEvent = () => { };

        public async override void Enter()
        {
            OnGameClearEvent?.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(11f));
            ScreenFader.Instance.FadeOut();
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
        }
    }
}
