using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace VRShooting
{
    public class Spider : Enemy
    {
        [SerializeField] float coeffcientOfDistance = 1f;
        [SerializeField] float targetLineThreshold = 1f;
        [SerializeField] float ThresholdDistanceFromPlayer = 2f;
        NavMeshAgent agent;


        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            var diffNormalized = (transform.position - playerEye.position).normalized;
            var target = diffNormalized * coeffcientOfDistance + playerEye.position;
            agent.SetDestination(target);
            agent.updateRotation = false;
        }


        public override void MUpdate()
        {
            base.MUpdate();

            var distanceFromPlayer = playerEye.position - transform.position;
            if (agent.remainingDistance < targetLineThreshold && distanceFromPlayer.magnitude < ThresholdDistanceFromPlayer)
            {
                AttackCheck();
            }
        }

        public override async void TakeDamage(int amount)
        {
            status.Hp -= amount;
            agent.isStopped = true;
            if (status.Hp <= 0)
            {
                animator.SetTrigger(AnimParam.ToDeath.ToString());
            }
            else
            {
                animator.SetTrigger(AnimParam.Damage.ToString());
                await PauseAsync(1000, () =>
                {
                    if (agent.isStopped)
                        agent.isStopped = false;
                });
            }
        }
    }
}