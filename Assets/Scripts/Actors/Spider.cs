using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VRShooting
{
    public class Spider : Enemy
    {

        NavMeshAgent agent;



        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            var onUnitCircle = CustomMath.onUnitCircle;
            var target = new Vector3(onUnitCircle.x, 0f, onUnitCircle.y) * status.AroundRange + player.position;
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = target;
            go.transform.localScale *= .2f;
            agent.SetDestination(target);
        }

        public override void MUpdate()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        agent.SetDestination(hit.point);
            //    }
            //}
        }

        public override void TakeDamage(int amount)
        {
            animator.SetTrigger(AnimParam.Damage.ToString());
            status.Hp -= amount;
            if (status.Hp <= 0)
            {
                agent.isStopped = true;
                animator.SetTrigger(AnimParam.ToDeath.ToString());
            }
        }
    }
}