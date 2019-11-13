using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace VRShooting
{
    public class Boss : Bee
    {
        protected new void MoveCheck()
        {
            var diff = transform.position - prevPos;
            var isMoving = animator.GetBool(AnimParam.IsMoving.ToString());

            var scalar = Vector3.Dot(diff, diff);
            if (scalar < status.MoveCheckThreshold)
            {
                forwardDirection = playerEye.position - transform.position;
                isMoving = false;
            }
            else
            {
                forwardDirection = diff.normalized;
                isMoving = true;
            }

            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
            {
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
            }

            if (myFlock.IsAllowAttack)
            {
                AttackCheck();
            }
            prevPos = transform.position;
        }
    }
}
