using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class CrossHair : ManagedMono
    {
        [SerializeField] Transform barrel;
        [SerializeField] [Range(0f, 1f)] float linearAmount;
        public override void MFixedUpdate()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, barrel.rotation, linearAmount);
        }
    }
}
