using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VRShooting
{
    /// <summary>
    /// Enemy
    /// </summary>
    public class Enemy : ManagedMono, IDamagable, IAttackable
    {
        /// <summary>Status</summary>   
        public EnemyStatus Status => status;
        public EnemyRole Role { get => role; set => role = value; }
        /// <summary>Velocity</summary>
        public Vector3 Velocity { get => velocity; set => velocity = value; }
        [SerializeField] [Header("敵のパラメーター")] protected EnemyStatus status;
        [SerializeField] protected EnemyRole role = EnemyRole.None;

        protected Animator animator;
        protected Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }
        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            MoveCheck();
        }

        /// <summary>
        /// ダメージ受ける処理
        /// </summary>
        /// <param name="amount"></param>
        public virtual void TakeDamage(int amount)
        {
            animator.SetTrigger(AnimTag.Damage.ToString());
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimTag.Death.ToString());
            }
        }

        /// <summary>
        /// 攻撃処理
        /// </summary>
        public virtual void Attack()
        {
            animator.SetTrigger(AnimTag.Attack.ToString());
        }

        /// <summary>
        /// 死ぬ処理
        /// </summary>
        public virtual void ToDie()
        {
            Destroy(gameObject);
        }

        /// <summary>前フレームの自分自身の座標</summary>
        Vector3 prevPos = Vector3.zero;
        /// <summary>
        /// 座標差分を取って動いているかどうかの判定処理
        /// </summary>
        protected virtual void MoveCheck()
        {
            var diff = transform.position - prevPos;
            if (diff == Vector3.zero)
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), false);
            }
            else
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), true);
            }
            prevPos = transform.position;
        }
    }

    /// <summary>
    /// Enemies tag.
    /// </summary>
    public enum EnemyTag
    {
        Enemy,
        Bee,
    }
    /// <summary>
    /// Enemies animation tag
    /// </summary>
    public enum AnimTag
    {
        Idle,
        Move,
        Attack,
        Damage,
        Death,
    }
    /// <summary>
    /// Enemies animation parameters
    /// </summary>
    public enum AnimParam
    {
        Speed,
        IsMoving,
    }
    /// <summary>
    /// 軍隊系の敵の役割
    /// </summary>
    public enum EnemyRole
    {
        /// <summary>役割の割当て無し</summary>
        None,
        /// <summary>ホスト</summary>
        Host,
        /// <summary>子供</summary>
        Child,
    }
}
