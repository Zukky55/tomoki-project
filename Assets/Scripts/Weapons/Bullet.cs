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

        [SerializeField] BulletStatus masterData;
        BulletStatus status;
        private Vector3 velocity;

        protected override void Awake()
        {
            base.Awake();
            status = Instantiate(masterData);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(EnemyTag.Enemy.ToString()))
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(masterData.Pow);
                Debug.Log($"Hit the {enemy.name}. and HP is {enemy.Status.Hp}!!");
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
            velocity = dir.normalized * masterData.Spd;
        }
        public void GiveInitialVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }
    }
}