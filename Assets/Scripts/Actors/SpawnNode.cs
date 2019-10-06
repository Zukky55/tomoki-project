using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class SpawnNode : MonoBehaviour
    {
        public bool IsSpawned
        {
            get { return isSpawned; }
            set { isSpawned = value; }
        }

        bool isSpawned;
    }
}