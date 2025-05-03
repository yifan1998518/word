using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostSpawnerAndMover : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnInterval = 3f;
    public float spawnAreaMinZ = -6f;
    public float spawnAreaMaxZ = 8f;
    public float spawnAreaMinY = 1.25f;
    public float spawnAreaMaxY = 4f;
    public float moveSpeed = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGhost();
            timer = 0f;
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f);
        }
    }

    void SpawnGhost()
    {
        // 生成位置：X固定-15，Y随机，Z随机
        Vector3 spawnPos = new Vector3(
            -15f,
            Random.Range(spawnAreaMinY, spawnAreaMaxY),
            Random.Range(spawnAreaMinZ, spawnAreaMaxZ)
        );

        // 计算指向原点(0,0,0)的水平方向
        Vector3 lookDirection = Vector3.zero - spawnPos;
        lookDirection.y = 0; // 锁定水平面

        Quaternion spawnRotation = Quaternion.LookRotation(lookDirection);

        GameObject ghost = Instantiate(ghostPrefab, spawnPos, spawnRotation);
        
        // 强制水平旋转（消除可能的倾斜）
        ghost.transform.rotation = Quaternion.Euler(0, spawnRotation.eulerAngles.y, 0);

        // 配置移动组件
        GhostAutoMove mover = ghost.GetComponent<GhostAutoMove>() ?? ghost.AddComponent<GhostAutoMove>();
        mover.speed = moveSpeed;
        
        // 调试标记
        Debug.Log($"鬼魂生成在：{spawnPos}，初始朝向：{ghost.transform.forward}");
    }
}

public class GhostAutoMove : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // 水平移动（锁定Y轴位置）
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        movement.y = 0;
        transform.position += movement;

        // 调试显示实时位置
        // Debug.Log($"鬼魂当前位置：{transform.position}");

        // 精确边界检测（X轴超过9时切换场景）
        if (transform.position.x > 9f)
        {
            Debug.Log("触发场景切换，当前X：" + transform.position.x);
            SceneManager.LoadScene("start");
        }
    }

    void OnDrawGizmos()
    {
        // 在场景视图中绘制移动方向
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }
}