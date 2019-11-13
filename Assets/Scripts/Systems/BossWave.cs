using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Threading;
using UniRx;
using UniRx.Async;
using System.Collections.Generic;
using System;


namespace VRShooting
{
    public class BossWave : StateBehaviour
    {
        GameObject boss;
        BeeFlock bossFlock;
        public async override void Enter()
        {
            boss = Instantiate(stageManager.Boss, new Vector3(10, 10, 10), Quaternion.identity);
            bossFlock = boss.GetComponent<BeeFlock>();
            await WaitCutAsync(bossFlock);
            stageManager.SetState(StageManager.GameState.GameClear);
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }

        /// <summary>
        /// 対象のカットを条件が満たされる迄待つ。
        /// 条件1, そのカットに出てくる敵が全滅。
        /// 条件2, そのカットを最後まで再生。
        /// </summary>
        /// <param name="flock"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        async UniTask WaitCutAsync(BeeFlock flock)
        {
            var elapsedTime = 0f;

            await UniTask.WaitWhile(() =>
            {
                elapsedTime += Time.deltaTime;
                if (flock.AnySurvivingBees)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
