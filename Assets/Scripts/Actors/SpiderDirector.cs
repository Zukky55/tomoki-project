using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VRShooting
{
    public class SpiderDirector : MonoBehaviour
    {
        [SerializeField] List<SpawnNode> spawnNodes;
        [SerializeField] GameObject spiderPrefab;

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
                var rndValue = Random.Range(0, spawnNodes.Count);
                indexes.Add(rndValue);
                var go = Instantiate(enemyPrefab, spawnNodes[rndValue].transform.position, Quaternion.identity);
                spawnedEnemies.Add(go.GetComponent<Spider>());
                i++;
            }
            return spawnedEnemies;
        }

        public Spider Spawn()
        {
            var node = spawnNodes[Random.Range(0, spawnNodes.Count - 1)];
            var go = Instantiate(spiderPrefab, node.transform.position, Quaternion.identity);
            return go.GetComponent<Spider>();
        }

        public List<Spider> Spawn(int amount) => Spawn(spiderPrefab, amount);

    }
}