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
        public int FirePow { get => firePow; set => firePow = value; }
        /// <summary>一発の攻撃力</summary>
        public GameObject Bullet { get => bullet; set => bullet = value; }

        [SerializeField] [Header("回転速度")] float rollSpd;
        [SerializeField] [Header("発射インターバル")] float fireInterval;
        [SerializeField] [Header("一発の攻撃力")] int firePow;
        [SerializeField] [Header("銃弾のプレハブ")] GameObject bullet;
    }
}
