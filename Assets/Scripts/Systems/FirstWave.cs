using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class FirstWave : StateBehaviour
    {
        [SerializeField] SpiderSpawner spiderSpawner;
        [SerializeField] int amount;
        [SerializeField] float interval;
        protected override void OnGameStateEnter(StageManager.GameState wave)
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
            if (wave != targetState) return;
            spiderSpawner.Spawn(amount);
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
