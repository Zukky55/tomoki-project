using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class GameOver : StateBehaviour
    {
        public override void Enter()
        {
            FadeNavigator.Instance.ChangeScene("Main");
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}
