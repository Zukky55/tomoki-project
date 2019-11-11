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
    public class FirstWave : StateBehaviour
    {
        [SerializeField]
        PlayableDirector BeesDirector;

        [SerializeField]
        PlayableAsset firstCut;

        [SerializeField]
        PlayableAsset secondCut;

        [SerializeField]
        PlayableAsset thirdCut;

        [SerializeField]
        BeeFlock firstFlock;

        [SerializeField]
        BeeFlock secondFlock;

        [SerializeField]
        BeeFlock thirdFlock;

        PlayableAsset currentPA;


        public async override void Enter()
        {
            // Call the first cut.
            Debug.Log($"{firstCut}よばれた");
            BeesDirector.Play(firstCut);
            await WaitCutAsync(firstFlock, firstCut);
            // Call the second cut.
            Debug.Log($"{secondCut}よばれた");
            BeesDirector.Play(secondCut);
            await WaitCutAsync(secondFlock, secondCut);
            // Call the third cut.
            Debug.Log($"{thirdCut}よばれた");
            BeesDirector.Play(thirdCut);
            await WaitCutAsync(thirdFlock, thirdCut);
            // Set the game state.
            Debug.Log($"SecondWaveへ");
            SetGameState(StageManager.GameState.SecondWave);
        }

        /// <summary>
        /// 対象のカットを条件が満たされる迄待つ。
        /// 条件1, そのカットに出てくる敵が全滅。
        /// 条件2, そのカットを最後まで再生。
        /// </summary>
        /// <param name="flock"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        async UniTask WaitCutAsync(BeeFlock flock, PlayableAsset pa)
        {
           var elapsedTime = 0f;

            await UniTask.WaitWhile(() => {
                elapsedTime += Time.deltaTime;
                if (flock.AnySurvivingBees && elapsedTime < pa.duration)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }

    }
}
