using UnityEngine;
using System.Collections;
using System;
using UniRx.Async;

namespace VRShooting
{
    public class GameClear : StateBehaviour
    {
        [SerializeField] MachineGun gun;
        public event Action OnGameClearEvent = () => { };

        bool flag = false;
        public async override void Enter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(11f));
            gun.OnHit += Gun_OnHit; ;
        }

        private void Gun_OnHit()
        {
            ScreenFader.Instance.FadeOut();
            gun.OnHit -= Gun_OnHit; ;
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}
