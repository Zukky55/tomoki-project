using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class Player : ManagedMono
    {
        [SerializeField] Transform barrel;

        public override void MFixedUpdate()
        {
            var diff = barrel.localRotation.eulerAngles.y - transform.localRotation.eulerAngles.y;
            transform.RotateAround(barrel.position, Vector3.up, diff);
        }
    }
}