using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class SecondWave : StateBehaviour
    {
        protected override void OnGameStateEnter(StageManager.GameState wave)
        {
            if (wave != targetState) return;
        }
        protected override void OnGameStateExecute(StageManager.GameState wave)
        {
            if (wave != targetState) return;
        }

        protected override void OnGameStateExit(StageManager.GameState wave)
        {
            if (wave != targetState) return;
        }
    }
}
