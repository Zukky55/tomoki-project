using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx.Async;

namespace VRShooting
{
    /// <summary>
    /// Enemy
    /// </summary>
    public abstract class Enemy : ManagedMono, IDamagable, IAttackable
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
        protected bool isAttackable = false;

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

        protected float elapsedTimeFromAttacked = 0f;
        /// <summary>
        /// 攻撃処理
        /// </summary>
        public virtual void AttackCheck()
        {
            elapsedTimeFromAttacked += Time.deltaTime;
            if (elapsedTimeFromAttacked < status.AttackInterval) return;
            elapsedTimeFromAttacked = 0f;
            Attack();
        }

        public virtual void Attack()
        {
            Debug.Log($"{gameObject.name}は攻撃しまーす");
            animator.SetTrigger(AnimParam.Attack.ToString());
            player.TakeDamage(status.Pow);
        }

        public void PlayAttackSE() => Instantiate(status.AttackSE, transform.position, Quaternion.identity);

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
        protected abstract void MoveCheck();
        protected virtual async UniTask PauseAsync(int delayMilliSecond,Action action)
        {
            await Task.Delay(delayMilliSecond);
            action.Invoke();
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
