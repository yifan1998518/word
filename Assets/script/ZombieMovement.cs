using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // 朝面朝方向移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}