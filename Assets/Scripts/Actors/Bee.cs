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
        protected override void Awake()
        {
            base.Awake();
            status.AttackInterval = status.AttackInterval + Random.Range(-5, 10);
            elapsedTimeFromAttacked = 0f;
        }
        public override void MUpdate()
        {
            base.MUpdate();
            if (!animator.GetBool(AnimParam.IsMoving.ToString()))
            {
                Attack();
            }
        }
    }
}