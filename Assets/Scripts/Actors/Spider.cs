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
            agent.SetDestination(player.position);
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
    }
}