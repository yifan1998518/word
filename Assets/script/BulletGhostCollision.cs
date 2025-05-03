using UnityEngine;

public class BulletGhostCollision : MonoBehaviour
{
    [Header("碰撞设置")]
    [Tooltip("匹配鬼魂对象的标签")] 
    public string ghostTag = "Ghost";

    [Header("音效设置")]
    public AudioClip ghostDeathSound;  // 拖入音频文件
    [Range(0, 1)] public float volume = 1f;

    void OnCollisionEnter(Collision collision)
    {
        ProcessCollision(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other.gameObject);
    }

    void ProcessCollision(GameObject hitObject)
    {
        if (hitObject.CompareTag(ghostTag))
        {
            // 在鬼魂位置播放音效
            PlayDeathSound(hitObject.transform.position);
            
            Destroy(hitObject);
            Destroy(gameObject, 0.01f);
        }
    }

    void PlayDeathSound(Vector3 position)
    {
        if (ghostDeathSound != null)
        {
            // 在3D空间播放音效
            AudioSource.PlayClipAtPoint(ghostDeathSound, position, volume);
        }
        else
        {
            Debug.LogWarning("未分配鬼魂死亡音效");
        }
    }
}