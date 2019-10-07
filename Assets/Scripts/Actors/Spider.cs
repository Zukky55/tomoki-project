using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
            var rnd = Random.onUnitSphere;
            var target = new Vector3(rnd.x, 0f, rnd.z) * status.AroundRange + player.position;
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = target;
            go.transform.localScale *= .2f;
            //agent.SetDestination(target);
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