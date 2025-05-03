using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnInterval = 3f;          // 初始生成间隔
    public int zombiesPerWave = 1;            // 每次生成的僵尸数量
    public int maxZombies = 100;              // 最大僵尸数量
    public float zombieSpeed = 2f;

    private float timer = 0f;
    private int totalZombiesSpawned = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && totalZombiesSpawned < maxZombies)
        {
            for (int i = 0; i < zombiesPerWave; i++)
            {
                SpawnZombie();
                totalZombiesSpawned++;
            }

            timer = 0f;

            // 逐渐减少间隔 & 增加生成数量
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f);
            zombiesPerWave++;
        }
    }

    void SpawnZombie()
    {
        float randomX = Random.Range(-15.18f, -0.01f);
        float randomZ = Random.Range(-0.4566f, 8.28f);
        Vector3 spawnPosition = new Vector3(randomX, 0.8f, randomZ);  // ✅ Y轴固定为0.8

        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.Euler(0, 90, 0)); // 朝前方

        Rigidbody rb = zombie.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = zombie.transform.forward * zombieSpeed;
        }
    }
}