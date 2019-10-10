using UnityEngine;
using System.Collections;

namespace VRShooting
{
    public class SecondWave : StateBehaviour
    {
        [SerializeField] SpiderDirector spiderSpawner;
        [SerializeField] int spawnEachAmount;
        [SerializeField] int spawnCount;
        [SerializeField] float interval;

        public override void Enter()
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
            Debug.Log($"FirstWaveきた");
            spiderSpawner.Spawn(spawnEachAmount);
        }


        float elapsedTime = 0f;
        public override void Execute()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < interval) return;
            elapsedTime = 0f;
            spiderSpawner.Spawn(spawnEachAmount);
        }

        public override void Exit()
        {
        }
    }
}
