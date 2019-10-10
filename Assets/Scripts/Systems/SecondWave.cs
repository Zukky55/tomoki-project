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
        /// <summary>指定回数分蜘蛛を出現させた後次のウェーブを呼ぶ迄の待機時間</summary>
        [SerializeField] float callNextWaveDelayTime = 5f;

        public override void Enter()
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
            Debug.Log($"FirstWaveきた");
            spiderSpawner.Spawn(spawnEachAmount);
        }


        int spawnedCount = 0;
        float elapsedTime = 0f;
        public override void Execute()
        {
            SpawnSpider();
        }

        public override void Exit()
        {
        }

        void SpawnSpider()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < interval || spawnedCount > spawnCount) return;
            elapsedTime = 0f;
            spiderSpawner.Spawn(spawnEachAmount);

            spawnedCount++;
            if (spawnedCount >= spawnCount)
            {
                StartCoroutine(TransitionWaveCoroutine(StageManager.GameState.BossWave, callNextWaveDelayTime));
                // incrementして判定に引っかかる様にする
                spawnedCount++;
            }
        }
    }
}
