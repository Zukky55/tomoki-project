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
        /// <summary>Role</summary>
        public EnemyRole Role { get => role; set => role = value; }
        /// <summary>Velocity</summary>
        public Vector3 Velocity { get => velocity; set => velocity = value; }

        /// <summary>Transform of Player.</summary>
        static protected Transform player;

        /// <summary>Status of enemy.</summary>
        [SerializeField] [Header("敵のパラメーター")] protected EnemyStatus masterData;
        [SerializeField] protected EnemyRole role = EnemyRole.None;
        /// <summary>進行方向から向きを取得する判断をする閾値</summary>
        [SerializeField, Range(0f, 1f)] protected float moveCheckThreshold;
        [SerializeField] SphereCollider playerDetectCollider;
        protected EnemyStatus status;
        protected Animator animator;
        protected Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            if (!player)
            {
                //player = GameObject.FindGameObjectWithTag("Player").transform;
                player = Camera.main.transform;
            }
            animator = GetComponent<Animator>();
            status = Instantiate(masterData);
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
            animator.SetTrigger(AnimParam.Damage.ToString());
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimParam.ToDeath.ToString());
            }
        }

        /// <summary>
        /// 攻撃処理
        /// </summary>
        public virtual void Attack()
        {
            animator.SetTrigger(AnimParam.Attack.ToString());
        }

        /// <summary>
        /// 死ぬ処理
        /// </summary>
        public virtual void ToDeath()
        {
            Destroy(gameObject);
        }

        /// <summary>進行している方向の単位ベクトル</summary>
        protected Vector3 forwardDirection = Vector3.zero;
        /// <summary>前フレームの自分自身の座標</summary>
        protected Vector3 prevPos = Vector3.zero;
        /// <summary>
        /// 座標差分を取って動いているかどうかの判定処理
        /// </summary>
        protected virtual void MoveCheck()
        {
            var diff = transform.position - prevPos;
            var isMoving = animator.GetBool(AnimParam.IsMoving.ToString());

            if (diff == Vector3.zero)
            {
                isMoving = false;
            }
            else if (Vector3.Dot(diff, diff) > moveCheckThreshold)
            {
                forwardDirection = diff.normalized;
                isMoving = false;
            }
            else
            {
                forwardDirection = player.position - transform.position;
                isMoving = true;
            }
            /// フラグ切り替えたフレームだけ<see cref="Animator.SetBool(string, bool)"/>する
            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
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
        IsMoving,
        Damage,
        Attack,
        ToDeath,
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
