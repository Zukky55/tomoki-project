using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Collections;

namespace VRShooting
{
    /// <summary>
    /// 蜂のベースクラス
    /// </summary>
    public class Bee : Enemy
    {
        [SerializeField] PlayableTrack pTrack;
        [SerializeField] PlayableDirector pDirector;
        [SerializeField] PlayableAsset pAsset;

        [SerializeField] ParticleSystem ps;
        /// <summary>方向転換の線形補間係数</summary>
        [SerializeField] float turnSpeed = .5f;

        public override void MUpdate()
        {
            base.MUpdate();
            var targetRot = Quaternion.LookRotation(forwardDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }

        protected override void MoveCheck()
        {
            var diff = transform.position - prevPos;
            var isMoving = animator.GetBool(AnimParam.IsMoving.ToString());

            if (diff == Vector3.zero)
            {
                isMoving = false;
            }
            else if (Vector3.Dot(diff, diff) > moveCheckThreshold)
            {
                forwardDirection = diff.normalized;
                isMoving = false;
            }
            else
            {
                forwardDirection = player.position - transform.position;
                isMoving = true;
            }
            /// フラグ切り替えたフレームだけ<see cref="Animator.SetBool(string, bool)"/>する
            if (animator.GetBool(AnimParam.IsMoving.ToString()) != isMoving)
                animator.SetBool(AnimParam.IsMoving.ToString(), isMoving);
            prevPos = transform.position;
        }
    }
}