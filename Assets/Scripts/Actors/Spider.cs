using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx.Async;
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
                agent.isStopped = true;
                AttackCheck();
            }
        }

        const float delayTime = 1;
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
                await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
                agent.isStopped = false;
            }
        }

        protected override void MoveCheck()
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
                isMoving = false;
            }
            /// フラグ切り替えたフレームだけ<see cref="Animator.SetBool(string, bool)"/>する
            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
            }
            prevPos = transform.position;
        }
    }
}