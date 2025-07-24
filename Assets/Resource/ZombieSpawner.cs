using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player;

    int baseHealth = 80;
    int baseDamage = 8;
    float baseSpeed = 3.5f;

    [Header("Spawn Settings")]
    public float spawnRadiusMin = 10f;
    public float spawnRadiusMax = 25f;
    public int zombiesPerWave = 5;
    public float spawnDelay = 0.5f;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI zombieCountText;

    private int currentWave = 0;
    private int aliveZombies = 0;

    private bool isSpawning = false;
    private bool waveFinished = true;

    [SerializeField] private int maxWave = 30;
    [SerializeField] private int maxZombiesPerWave = 50;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        if (currentWave >= maxWave)
        {
            Debug.Log("최대 웨이브 도달!");
            yield break;
        }
        if (zombiePrefab == null)
        {
            Debug.Log("zombiePrefab is missing! check zombispawner component.");
            yield break;
        }
        Debug.Log($"Spawning Wave: {currentWave + 1}");
        if (isSpawning)
            yield break;

        isSpawning = true;
        waveFinished = false;
        currentWave++;
        UpdateWaveUI();

        int zombiesToSpawn = Mathf.Min(zombiesPerWave + currentWave * 3, maxZombiesPerWave);
        aliveZombies = zombiesToSpawn;
        UpdateZombieCountUI(); //웨이브 시작 시
        double health = baseHealth + currentWave + 0.7; //웨이브 시작 할 때마다 좀비 체력 증가
        double damage = baseDamage + currentWave + 0.3; //웨이브 시작 할 때마다 좀비 데미지 증가
        float Speed = Mathf.Min(baseSpeed + currentWave * 0.1f, 6.0f); //웨이브 시작 할 때마다 좀비 속도 증가

        Debug.Log($"Trying to spawn {zombiesToSpawn} zombies");
        for (int i = 0; i < zombiesToSpawn; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            Debug.Log($"Spawn attempt #{i} at {spawnPos}");

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                GameObject zombie = Instantiate(zombiePrefab, hit.position, Quaternion.identity);
                ZombieAI ai = zombie.GetComponent<ZombieAI>();
                if (ai != null)
                {
                    Debug.Log($"Zombie AI found, subscribing to OnZombieDeath");
                    ai.maxHealth = health;
                    ai.damage = damage;
                    ai.moveSpeed = Speed;
                    ai.OnZombieDeath -= OnZombieKilled;
                    ai.OnZombieDeath += OnZombieKilled;
                }
                else
                {
                    Debug.LogWarning("ZombieAI component missing on zombiePrefab!");
                }
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        isSpawning = false;
    }

    void OnZombieKilled()
    {
        aliveZombies--;
        Debug.Log($"Zombie Killed! Remaining: {aliveZombies}");
        UpdateZombieCountUI();

        if (aliveZombies <= 0)
        {
            Debug.Log($"All Zombies killed! Starting next wave.");
            StartCoroutine(WaitAndStartNewWave());
        }
    }

    IEnumerator WaitAndStartNewWave()
    {
        yield return new WaitForSeconds(2f);
        isSpawning = false;
        StartCoroutine(SpawnWave());
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPos;
        int attempts = 0;
        do
        {
            Vector2 randomPos = Random.insideUnitCircle * spawnRadiusMax;
            spawnPos = new Vector3(player.position.x + randomPos.x, player.position.y, player.position.z + randomPos.y);
            attempts++;
        } while (Vector3.Distance(spawnPos, player.position) < spawnRadiusMin && attempts < 10);

        return spawnPos;
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWave}";
        }
    }

    void UpdateZombieCountUI()
    {
        if (zombieCountText != null)
            zombieCountText.text = $"Zombies Left: {aliveZombies}";
    }
}

