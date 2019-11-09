using UnityEngine;
using System.Collections;
using System;

namespace VRShooting
{
    public class GameClear : StateBehaviour
    {
        public event Action OnGameClearEvent = () => { };

        public override void Enter()
        {
            OnGameClearEvent?.Invoke();
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}
