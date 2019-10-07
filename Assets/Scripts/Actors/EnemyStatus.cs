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
        public int Pow { get => pow; set => pow = value; }
        /// <summary>defense</summary>
        public int Def { get => def; set => def = value; }
        /// <summary>speed</summary>
        public float Spd { get => spd; set => spd = value; }
        public float AroundRange{ get => aroundRange; set => aroundRange = value; }
        

        [SerializeField] [Header("Hit Point")] int hp;
        [SerializeField] [Header("Power")] int pow;
        [SerializeField] [Header("Defense")] int def;
        [SerializeField] [Header("Speed")] float spd;
        [SerializeField] [Header("Coefficient of AroundRange")] float aroundRange;

    }
}