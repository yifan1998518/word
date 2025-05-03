using UnityEngine;

public class ZombieSpawnerAndMover : MonoBehaviour
{
    public GameObject zombiePrefab;       // 僵尸预制体
    public float spawnInterval = 3f;      // 初始生成间隔
    public float spawnAreaMinX = -15.18f;
    public float spawnAreaMaxX = -0.01f;
    public float spawnAreaMinZ = -0.4566f;
    public float spawnAreaMaxZ = 8.28f;
    public float zombieY = 0.8f;          // Y轴高度
    public float moveSpeed = 2f;          // 僵尸移动速度

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f;

            // 生成间隔逐渐减小（提高生成频率），最低限制为1秒
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f);
        }
    }

    void SpawnZombie()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaMinX, spawnAreaMaxX),
            zombieY,
            Random.Range(spawnAreaMinZ, spawnAreaMaxZ)
        );

        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.Euler(0, 90f, 0));

        // 添加自动移动组件（如果没有的话）
        if (!zombie.GetComponent<ZombieAutoMove>())
        {
            ZombieAutoMove mover = zombie.AddComponent<ZombieAutoMove>();
            mover.speed = moveSpeed;
        }
    }
}

public class ZombieAutoMove : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // 始终保持朝向 Y 轴为 90 度
        transform.rotation = Quaternion.Euler(0, 90f, 0);

        // 确保朝着 Z 轴正方向移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
