using System;
using System.Collections;
using UnityEngine;

namespace VRShooting
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemyStatus", menuName = "Statuses/Enemy")]
    public class EnemyStatus : ScriptableObject
    {
        /// <summary>hit point</summary>
        public int Hp { get => hp; set => hp = value; }
        /// <summary>power</summary>
        public float Pow { get => pow; set => pow = value; }
        /// <summary>defense</summary>
        public float Def { get => def; set => def = value; }
        /// <summary>speed</summary>
        public float Spd { get => spd; set => spd = value; }

        [SerializeField] [Header("Hit Point")] int hp;
        [SerializeField] [Header("Power")] float pow;
        [SerializeField] [Header("Defense")] float def;
        [SerializeField] [Header("Speed")] float spd;
    }
}