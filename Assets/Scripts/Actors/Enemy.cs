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
        /// <summary>Velocity</summary>
        public Vector3 Velocity { get => velocity; set => velocity = value; }

        /// <summary>Transform of Player.</summary>
        static protected Transform playerEye;
        static protected Player player;


        /// <summary>Status of enemy.</summary>
        [SerializeField] [Header("敵のパラメーター")] protected EnemyStatus masterData;

        protected EnemyStatus status;
        protected Animator animator;
        protected Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            if (!playerEye) playerEye = Camera.main.transform;
            if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            animator = GetComponent<Animator>();
            status = Instantiate(masterData);

            prevPos = transform.position;
            elapsedTimeFromAttacked = status.AttackInterval;
        }
        /// <summary>
        /// Managed Update
        /// </summary>
        public override void MUpdate()
        {
            MoveCheck();
            ForwardAdjustment();
        }

        protected virtual void ForwardAdjustment()
        {
            var targetRot = Quaternion.LookRotation(forwardDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * status.TurnSpeed);
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

       protected float elapsedTimeFromAttacked = 0f;
        /// <summary>
        /// 攻撃処理
        /// </summary>
        public virtual void Attack()
        {
            elapsedTimeFromAttacked += Time.deltaTime;
            if (elapsedTimeFromAttacked < status.AttackInterval) return;
            elapsedTimeFromAttacked = 0f;
            animator.SetTrigger(AnimParam.Attack.ToString());
            player.TakeDamage(status.Pow);
            Debug.Log($"TakeDamaaaaaaaaaaaage");
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
                isMoving = true;
            }
            /// フラグ切り替えたフレームだけ<see cref="Animator.SetBool(string, bool)"/>する
            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
            {
                Debug.Log($"{gameObject.name},{isMoving}");
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
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
        Spider,
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
}
