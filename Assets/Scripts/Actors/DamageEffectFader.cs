using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class DamageEffectFader : ManagedMono
    {
        [SerializeField]
        GameObject fadeSphere;
        [SerializeField]
        Animator animator;

        public void Fade() =>animator.SetTrigger("Fade");
        public void Death()
        {
            animator.SetBool("IsDied",true);
            animator.SetTrigger("Death");
        }
    }
}