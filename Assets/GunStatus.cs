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
        public float RollSPD { get => rollSPD; set => rollSPD = value; }
        /// <summary>発射速度</summary>
        public float FireSPD { get => fireSPD; set => fireSPD = value; }
        /// <summary>一発の攻撃力</summary>
        public float FirePOW { get => firePOW; set => firePOW = value; }
        /// <summary>一発の攻撃力</summary>
        public GameObject Bullet { get => bullet; set => bullet = value; }

        [SerializeField] [Header("回転速度")] float rollSPD;
        [SerializeField] [Header("発射速度")] float fireSPD;
        [SerializeField] [Header("一発の攻撃力")] float firePOW;
        [SerializeField] [Header("銃弾のプレハブ")] GameObject bullet;
    }
}
