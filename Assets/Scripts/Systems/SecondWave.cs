using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UniRx.Async;
using System.Collections.Generic;
using System;

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

        List<Spider> spiders = new List<Spider>();

        public override void Enter()
        {
            // 蜂の群れのtimelineを複数パターン作って、ランダムに活かせるかもしくはその数文順番に活かせる
            spiderSpawner.Spawn(spawnEachAmount);
            for (int i = 0; i < spawnEachAmount; i++)
            {
                spiders.Add(spiderSpawner.Spawn());
            }
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

        bool isCalled;
        async void SpawnSpider()
        {
            if (isCalled) return;
            elapsedTime += Time.deltaTime;
            if (elapsedTime < interval || spawnedCount > spawnCount) return;
            elapsedTime = 0f;
            spiders.Add(spiderSpawner.Spawn());

            spawnedCount++;
            if (spawnedCount >= spawnCount )
            {
                await UniTask.Delay(TimeSpan.FromSeconds(8f));
                SetGameState(StageManager.GameState.BossWave);
                // incrementして判定に引っかかる様にする
                isCalled = true;
                spawnedCount++;
            }
        }
    }
}
