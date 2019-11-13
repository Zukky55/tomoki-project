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

        float time;
        public override void MUpdate()
        {
            base.MUpdate();

            var distanceFromPlayer = playerEye.position - transform.position;
            var isNearbly = distanceFromPlayer.magnitude < ThresholdDistanceFromPlayer;
            //Debug.Log($"distanceFromPlayer.magnitude{distanceFromPlayer.magnitude} < ThresholdDistanceFromPlayer{ThresholdDistanceFromPlayer} " +
                //$"= {isNearbly}|");
            
            if (/*agent.remainingDistance < targetLineThreshold && */isNearbly)
            {
                agent.isStopped = true;
                AttackCheck();
            }
        }

        const float delayTime = 2;
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
                if (agent != null) agent.isStopped = false;
            }
        }

        protected override void MoveCheck()
        {
            var diff = transform.position - prevPos;
            var isMoving = animator.GetBool(AnimParam.IsMoving.ToString());
            var scalar = Vector3.Dot(diff, diff);

            Debug.Log($"scalar  = {scalar }<MoveCheckThreshold= {status.MoveCheckThreshold} = {scalar < status.MoveCheckThreshold}");
            if (scalar < Mathf.Epsilon)
            {
                forwardDirection = playerEye.position - transform.position;
                isMoving = false;
            }
            else
            {
                forwardDirection = diff.normalized;
                isMoving = true;
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