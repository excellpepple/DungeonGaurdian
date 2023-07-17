using Lean.Transition;
using Systems.Health;

using UnityEngine;
using UnityEngine.Events;

namespace Systems.Spawner
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float spawnChance;
    }

    public class SpawnManager : MonoBehaviour
    {
        public int enemiesPerWave = 4;
        public EnemyType[] enemyTypes;
        public UnityEvent OnEnemyDeath;
        public UnityEvent OnWaveEnd;
        public Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f);
        public float spawnDelay = 2f;
        public LeanManualAnimation WaveEndEffect;
        private int currentWave = 1;
        private int enemiesToSpawn;
        private int enimiesRemaining;
        private float nextSpawnTime;

        public int GetCurrentEnemyCount()
        {
            return enimiesRemaining;
        }

        public int GetCurrentWave()
        {
            return currentWave;
        }

        private void EndWave()
        {
            WaveEndEffect.BeginTransitions();
            OnWaveEnd.Invoke();
            currentWave++;
            StartWave();
        }

        private void EnemyDeath()
        {
            enimiesRemaining--;
            OnEnemyDeath.Invoke();
        }

        private void SpawnEnemy()
        {
            if (enemyTypes.Length == 0)
            {
                return;
            }

            // Determine the enemy type to spawn based on their spawn chances
            float totalSpawnChance = 0f;
            for (int i = 0; i < enemyTypes.Length; i++)
            {
                totalSpawnChance += enemyTypes[i].spawnChance;
            }

            float randomValue = Random.Range(0f, totalSpawnChance);
            float cumulativeChance = 0f;
            GameObject enemyPrefab = null;

            for (int i = 0; i < enemyTypes.Length; i++)
            {
                cumulativeChance += enemyTypes[i].spawnChance;
                if (randomValue <= cumulativeChance)
                {
                    enemyPrefab = enemyTypes[i].enemyPrefab;
                    break;
                }
            }

            if (enemyPrefab == null)
            {
                Debug.LogWarning("Failed to select an enemy type");
                return;
            }

            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
                Random.Range(-spawnAreaSize.y, spawnAreaSize.y),
                Random.Range(-spawnAreaSize.z, spawnAreaSize.z));

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            /*EnemyLevel enemyLevel = newEnemy.GetComponent<EnemyLevel>();
            if (enemyLevel != null)
            {
                // Calculate the enemy level based on the current wave count
                int minLevel = Mathf.Max(1, currentWave - 2);
                int maxLevel = currentWave + 2;
                int randomLevel = Random.Range(minLevel, maxLevel + 1);

                enemyLevel.SetLevel(randomLevel);
            }*/

            Health.Health enemyhealth = newEnemy.GetComponent<Health.Health>();

            enemyhealth.OnDie += EnemyDeath;

            //enemyhealth.ChangeHealth(enemyhealth.GetCurrentHealth() * ((float)currentWave));

            enemiesToSpawn--;
        }

        private void Start()
        {
            StartWave();
        }

        private void StartWave()
        {
            enemiesToSpawn = enemiesPerWave;
            enimiesRemaining = enemiesPerWave;
            nextSpawnTime = Time.time;
        }

        private void Update()
        {
            if (enemiesToSpawn > 0)
            {
                if (Time.time >= nextSpawnTime)
                {
                    SpawnEnemy();
                    nextSpawnTime = Time.time + spawnDelay;
                }
            }
            if (enimiesRemaining == 0)
            {
                EndWave();
            }
        }
    }
}