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
        [SerializeField] float myVar;

        public float MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        Animator animator;
        private Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }
        public override void MUpdate()
        {
            MoveCheck();
        }


        public void TakeDamage(int amount)
        {
            animator.SetTrigger(AnimTag.Damage.ToString());
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimTag.Death.ToString());
            }
        }
        public void Attack()
        {
            animator.SetTrigger(AnimTag.Attack.ToString());
        }

        public void ToDie()
        {
            Destroy(gameObject);
        }

        Vector3 prevPos = Vector3.zero;
        void MoveCheck()
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
    public enum AnimParam
    {
        Speed,
        IsMoving,
    }
}
