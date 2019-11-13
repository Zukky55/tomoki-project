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
        public void CrearGame() => StageManager.Instance.SetState(StageManager.GameState.GameClear);

        protected override void MoveCheck()
        {
            var diff = transform.position - prevPos;
            var isMoving = animator.GetBool(AnimParam.IsMoving.ToString());

            if (diff == Vector3.zero)
            {
                forwardDirection = playerEye.position - transform.position;
                isMoving = false;
            }
            else if (Vector3.Dot(diff, diff) >= status.MoveCheckThreshold)
            {
                forwardDirection = diff.normalized;
                isMoving = true;
            }
            else
            {
                forwardDirection = playerEye.position - transform.position;
                isMoving = false;
            }
            /// フラグ切り替えたフレームだけ<see cref="Animator.SetBool(string, bool)"/>する
            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
            }
            prevPos = transform.position;
        }
    }
}