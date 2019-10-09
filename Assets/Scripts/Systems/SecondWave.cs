using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class SecondWave : StateBehaviour
    {
        [SerializeField] SpiderSpawner spiderSpawner;
        [SerializeField] int amount;
        [SerializeField] float interval;

        public async override void Enter()
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
            Debug.Log($"FirstWaveきた");
            spiderSpawner.Spawn(amount);
            //await TransitionWaveAsync(StageManager.GameState.SecondWave);
            //StartCoroutine(TransitionWaveCoroutine(StageManager.GameState.SecondWave));
        }


        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}
