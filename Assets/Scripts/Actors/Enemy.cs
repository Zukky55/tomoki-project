using UnityEngine;
using System.Collections;

namespace VRShooting
{
    /// <summary>
    /// Enemy
    /// </summary>
    public class Enemy : ManagedMono, IDamagable
    {
        /// <summary>Status</summary>   
        public EnemyStatus Status => status;

        [SerializeField] [Header("敵のパラメーター")] EnemyStatus status;

        Animator animator;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }
        public void TakeDamage(int amount)
        {
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                Destroy(this);
            }
        }

        public override void MFixedUpdate()
        {

        }
    }

    public enum EnemyTag
    {
        Enemy,
        Bee,
    }
}
