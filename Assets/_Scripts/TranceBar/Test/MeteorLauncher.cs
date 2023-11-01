using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bardent
{
    public class MeteorLauncher : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        public Transform spawnPoint;
        public float launchSpeed = 5f;
        public string playerTag = "Player";
        public float minSpawnInterval = 1f; // Minimum time between spawns
        public float maxSpawnInterval = 4f; // Maximum time between spawns
        public HealthBar healthBar;

        private void Start()
        {
            // Start spawning prefabs at random intervals
            StartSpawning();
        }

        void StartSpawning()
        {
            // Choose a random time interval between minSpawnInterval and maxSpawnInterval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Spawn the prefab and set up the next spawn
            SpawnAndLaunch();
            Invoke("StartSpawning", spawnInterval);
        }

        void SpawnAndLaunch()
        {
            // Spawn the prefab at the spawn point
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

            // Set the launch direction to straight down
            Vector2 launchDirection = Vector2.down;

            // Apply force to the prefab to launch it downward
            Rigidbody2D prefabRigidbody = spawnedPrefab.GetComponent<Rigidbody2D>();
            if (prefabRigidbody != null)
            {
                prefabRigidbody.velocity = launchDirection.normalized * launchSpeed;
            }
        }



    }
}
