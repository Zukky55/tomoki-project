using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class FirstWave : StateBehaviour
    {
        protected override void OnGameStateEnter(StageManager.GameState wave)
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
        }
        protected override void OnGameStateExecute(StageManager.GameState wave)
        {
        }
        protected override void OnGameStateExit(StageManager.GameState wave)
        {
        }
    }
}
