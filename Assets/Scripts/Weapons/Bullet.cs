using UnityEngine;
using System.Collections;
using System.Linq;
using System;

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
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tag.Enemy.ToString()))
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
        public override void MUpdate()
        {
            if (transform.position.magnitude > 100)
            {
                Destroy(gameObject);
            }
        }

        public void VoluntaryImpulse(Vector3 spawnPos)
        {
            var dir = transform.position - spawnPos;
            velocity = dir.normalized * status.Spd;
        }
        public void GiveInitialVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }
    }
}