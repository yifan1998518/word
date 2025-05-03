using UnityEngine;
using System.Collections;

public class SpecialSingleGhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;  // 需要拖入相同的鬼魂预制体
    public float waitTime = 30f;    // 等待时间
    public float scaleMultiplier = 2.5f; // 尺寸倍数
    public float specialMoveSpeed = 3f;  // 特殊移动速度

    // 生成区域设置（与原脚本保持相同）
    public float spawnAreaMinZ = -6f;
    public float spawnAreaMaxZ = 8f;
    public float spawnAreaMinY = 1.25f;
    public float spawnAreaMaxY = 4f;

    void Start()
    {
        StartCoroutine(SpawnSpecialGhost());
    }

    IEnumerator SpawnSpecialGhost()
    {
        yield return new WaitForSeconds(waitTime);
        
        // 生成位置计算
        Vector3 spawnPos = new Vector3(
            -15f,
            Random.Range(spawnAreaMinY, spawnAreaMaxY),
            Random.Range(spawnAreaMinZ, spawnAreaMaxZ)
        );

        // 计算朝向原点
        Vector3 lookDirection = Vector3.zero - spawnPos;
        lookDirection.y = 0;
        Quaternion spawnRotation = Quaternion.LookRotation(lookDirection);

        // 实例化并调整属性
        GameObject ghost = Instantiate(ghostPrefab, spawnPos, spawnRotation);
        ghost.transform.localScale *= scaleMultiplier;
        ghost.transform.rotation = Quaternion.Euler(0, spawnRotation.eulerAngles.y, 0);

        // 配置移动组件
        GhostAutoMove mover = ghost.GetComponent<GhostAutoMove>() ?? ghost.AddComponent<GhostAutoMove>();
        mover.speed = specialMoveSpeed;

        Debug.Log($"特殊鬼魂已生成！位置：{spawnPos}\n" +
                 $"尺寸：{ghost.transform.localScale}\n" +
                 $"速度：{specialMoveSpeed}");
    }
}