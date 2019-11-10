using System;
using UnityEngine;
using System.Collections;

namespace VRShooting
{
    [Serializable]
    [CreateAssetMenu(fileName = "GunStatus", menuName = "Statuses/Gun")]
    public class GunStatus : ScriptableObject
    {
        /// <summary>旋回速度</summary>
        public float RollSpd { get => rollSpd; set => rollSpd = value; }
        /// <summary>発射インターバル</summary>
        public float FireInterval { get => fireInterval; set => fireInterval = value; }
        /// <summary>一発の攻撃力</summary>
        public int BulletPow { get => bulletPow; set => bulletPow = value; }
        /// <summary>銃弾の速度</summary>
        public float BulletSpd { get => bulletSpd; set => bulletSpd = value; }
        /// <summary>銃倉</summary>
        public int Magazine  => magazine; 


        [SerializeField] [Header("回転速度")] float rollSpd;
        [SerializeField] [Header("発射インターバル")] float fireInterval;
        [SerializeField] [Header("一発の攻撃力")] int bulletPow;
        [SerializeField] [Header("銃弾の速度")] float bulletSpd;
        [SerializeField] [Header("銃倉")] int magazine;
    }
}
