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

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public override void ManagedUpdate()
        {

        }



                
        public void TakeDamage(int amount)
        {
            status.Hp -= amount;
            if(status.Hp <= 0)
            {
                Destroy(this);
            }
        }
    }
}
