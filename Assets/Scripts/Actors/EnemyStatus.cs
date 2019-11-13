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
        public float Speed { get => speed; set => speed = value; }
        /// <summary>AroundRange</summary>
        public float AroundRange { get => aroundRange; set => aroundRange = value; }
        /// <summary>方向転換の線形補間係数</summary>
        public float TurnSpeed { get => turnSpeed; set => turnSpeed = value; }
        /// <summary>進行方向から向きを取得する判断をする閾値</summary>
        public float MoveCheckThreshold { get => moveCheckThreshold; set => moveCheckThreshold = value; }
        public float AttackInterval { get => attackInterval; set => attackInterval = value; }
        public GameObject AttackSE{ get => attackSE; set => attackSE = value; }


        [SerializeField] [Header("Hit Point")] int hp;
        [SerializeField] [Header("Power")] int pow;
        [SerializeField] [Header("Defense")] int def;
        [SerializeField] [Header("Speed")] float speed;
        [SerializeField] [Header("Coefficient of AroundRange")] float aroundRange;
        [SerializeField] [Header("Linear interpolation factor for direction change.")] float turnSpeed = 1f;
        [SerializeField] [Header("Move amount threshold")]  protected float moveCheckThreshold;
        [SerializeField] [Header("Interval time of Attack")] float attackInterval;
        [SerializeField] [Header("Sound effect on Attack.")] GameObject attackSE;


    }
}