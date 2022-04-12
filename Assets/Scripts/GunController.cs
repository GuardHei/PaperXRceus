using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrototype;
    public Transform firePoint;
    public AudioClip sfx;
    public Inventory inventory;
    public float bulletSpeed;
    [Range(0f, 5f)]
    public float fireInterval = .5f;
    public float lastFireTime = -1f;

    public bool CanFire => (Time.time - lastFireTime > fireInterval) && inventory.basicAmmo > 0;

    private void Awake()
    {
        if (!inventory) inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
    }

    public void Fire()
    {
        if (!CanFire) return;

        lastFireTime = Time.time;

        var bullet = Instantiate(bulletPrototype, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletController>().speed = bulletSpeed;
    }
}
