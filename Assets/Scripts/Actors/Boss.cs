using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace VRShooting
{
    public class Boss : Enemy
    {
        PlayableDirector director;
        BeeFlock myFlock;
        const float attackableBoderline = 0.03f;
        protected override void Awake()
        {
            base.Awake();
            director = transform.parent.GetComponent<PlayableDirector>();
            myFlock = gameObject.GetComponentInParent<BeeFlock>();
            elapsedTimeFromAttacked = 0f;
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

            var scalar = Vector3.Dot(diff, diff);
            if (scalar < status.MoveCheckThreshold)
            {
                forwardDirection = playerEye.position - transform.position;
                isMoving = false;
            }
            else
            {
                forwardDirection = diff.normalized;
                isMoving = true;
            }

            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
            }

            // 群れから攻撃許可がおりている時自身が攻撃可能かどうか判断する
            if (myFlock.IsAllowAttack)
            {
                isAttackable = scalar > attackableBoderline;
            }

            prevPos = transform.position;
    }
    }
}