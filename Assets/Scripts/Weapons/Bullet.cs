using UnityEngine;
using System.Collections;
using System.Linq;

namespace VRShooting
{
    /// <summary>
    /// 銃弾
    /// </summary>
    public class Bullet : ManagedMono
    {
        /// <summary>銃弾のパラメーター</summary>
        public BulletStatus Status => status;
        /// <summary>Velocity</summary>
        public Vector3 Velocity { get => velocity; set => velocity = value; }

        [SerializeField] BulletStatus status;
        private Vector3 velocity;

        private void Start()
        {
            Destroy(gameObject);
        }

        public void Init(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(EnemyTag.Enemy.ToString()))
            {
                var enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(status.Pow);
                Destroy(gameObject);
            }
        }
        public override void MFixedUpdate()
        {
            transform.position += velocity;
        }
        public override void MUpdate() { }
    }
}