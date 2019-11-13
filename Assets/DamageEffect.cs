using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class DamageEffect : ManagedMono
    {
        [SerializeField]
        GameObject fadeSphere;
        [SerializeField]
        Animator animator;

        public void Fade() => animator.SetTrigger("Fade");
    }
}