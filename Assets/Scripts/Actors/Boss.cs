using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace VRShooting
{
    public class Boss : Enemy
    {
        PlayableDirector director;

        protected override void Awake()
        {
            base.Awake();
            director = transform.parent.GetComponent<PlayableDirector>();
        }

        /// <summary>
        /// ダメージ受ける処理
        /// </summary>
        /// <param name="amount"></param>
        public override async void TakeDamage(int amount)
        {
            status.Hp -= amount;
            director.Pause();
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimParam.ToDeath.ToString());
                director.Stop();
            }
            else
            {
                animator.SetTrigger(AnimParam.Damage.ToString());
                await PauseAsync(1000, () =>
                {
                    director.Resume();
                });
            }
        }
        public void CrearGame() => StageManager.Instance.SetState(StageManager.GameState.GameCrear);
    }
}