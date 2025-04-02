using UnityEngine;

public class AutoAddColliders : MonoBehaviour
{
    // 确保在编辑模式下也能自动运行
    void OnValidate()
    {
        // 遍历每个子物体
        foreach (Transform child in transform)
        {
            // 如果没有碰撞体（Collider），就添加 BoxCollider
            if (child.GetComponent<Collider>() == null)
            {
                // 添加 BoxCollider，或者根据需要改为其他类型的 Collider
                BoxCollider boxCollider = child.gameObject.AddComponent<BoxCollider>();
                
                // 可选：自动调整 BoxCollider 的大小以适应物体
                boxCollider.size = child.GetComponent<MeshRenderer>()?.bounds.size ?? new Vector3(1, 1, 1);
            }
        }
    }
}