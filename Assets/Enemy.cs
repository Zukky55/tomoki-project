using UnityEngine;
using System.Collections;

namespace VRShooting
{
    /// <summary>
    /// Enemy
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>Status</summary>
        public EnemyStatus Status => status;

        [SerializeField][Header("敵のパラメーター")] EnemyStatus status;

    }
}