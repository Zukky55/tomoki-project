using System;
using System.Collections;
using UnityEngine;

namespace VRShooting
{
    [Serializable]
    [CreateAssetMenu(fileName = "BulletStatus", menuName = "Statuses/Bullet")]
    public class BulletStatus : ScriptableObject
    {
        /// <summary>power</summary>
        public int Pow { get => pow; set => pow = value; }
        /// <summary>speed</summary>
        public float Spd { get => spd; set => spd = value; }

        [SerializeField] [Header("Power")] int pow;
        [SerializeField] [Header("Speed")] float spd;
    }
}