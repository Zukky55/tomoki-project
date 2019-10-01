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
        [SerializeField] [Header("敵のパラメーター")] EnemyStatus status;

        Animator animator;
        private Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }
        public void TakeDamage(int amount)
        {
            animator.SetTrigger(AnimTag.Damage.ToString());
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                ToDie();
            }
        }
        public void Attack()
        {
            animator.SetTrigger(AnimTag.Attack.ToString());
        }

        public void ToDie()
        {
            animator.SetTrigger(AnimTag.Death.ToString());
            var clipInfo = animator.GetNextAnimatorClipInfo(0);
            Debug.Log(clipInfo.);
        }

        public override void MFixedUpdate()
        {
            base.MFixedUpdate();
        }


    }

    public enum EnemyTag
    {
        Enemy,
        Bee,
    }
    public enum AnimTag
    {
        Idle,
        Move,
        Attack,
        Damage,
        Death,
    }
}
