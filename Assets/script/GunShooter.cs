using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooter : MonoBehaviour
{
    public GameObject bulletPrefab;   // 子弹预制体
    public Transform firePoint;       // 发射位置
    public float bulletForce = 50f;   // 子弹初速度

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void Update()
    {
        if (isHeld && Input.GetMouseButtonDown(0)) // 鼠标左键发射
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletForce;
        }
        Destroy(bullet, 5f); // 5 秒后销毁子弹
    }
}