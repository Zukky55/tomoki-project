using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace VRShooting
{
    public class GameOver : StateBehaviour
    {
        public async override void Enter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(7.2f));
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
