﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRShooting
{
    public class SpiderSpawner : MonoBehaviour
    {
        [SerializeField] List<SpawnNode> spawnNodes;
        [SerializeField] GameObject spider;

        public List<Spider> Spawn(GameObject enemyPrefab, int amount)
        {
            var spawnedEnemies = new List<Spider>();
            var indexes = new List<int>();
            foreach (var node in spawnNodes)
            {
                node.IsSpawned = false;
            }
            for (int i = 0; i < amount;)
            {
                var rndValue = UnityEngine.Random.Range(0, spawnNodes.Count);
                if (!indexes.Contains(rndValue))
                {
                    indexes.Add(rndValue);
                    var go = Instantiate(enemyPrefab, spawnNodes[rndValue].transform.position, Quaternion.identity);
                    spawnedEnemies.Add(go.GetComponent<Spider>());
                    i++;
                }
                else
                {
                    continue;
                }
            }
            return spawnedEnemies;
        }

        public List<Spider> Spawn(int amount) => Spawn(spider, amount);
    }
}