using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Collections;

namespace VRShooting
{
    /// <summary>
    /// 蜂のベースクラス
    /// </summary>
    public class Bee : Enemy
    {
        /// <summary>
        /// 自分の群れ
        /// </summary>
        BeeFlock myFlock;
        const float attackableBoderline = 0.03f;

        protected override void Awake()
        {
            base.Awake();
            status.AttackInterval = status.AttackInterval + Random.Range(-5, 10);
            elapsedTimeFromAttacked = 0f;
            myFlock = gameObject.GetComponentInParent<BeeFlock>();
        }
        public override void MUpdate()
        {
            base.MUpdate();
            if (isAttackable)
            {
                AttackCheck();
            }
        }
        public override void TakeDamage(int amount)
        {
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimParam.ToDeath.ToString());
            }
            else
            {
                animator.SetTrigger(AnimParam.Damage.ToString());
            }
        }

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