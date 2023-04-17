using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Slider shootSlider;

    private Rigidbody2D rb2d;
    private float bulletSpeed = 40f;
    private float cooldown = 0.2f;
    private float currCooldown = 0.0f;
    private bool shootReady;

    public static int eggCount = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessBulletSpawn();
    }

    private void ProcessBulletSpawn()
    {
        if (currCooldown >= cooldown)
        {
            shootReady = true;
        } else
        {
            shootReady = false;
            currCooldown += Time.deltaTime;
            currCooldown = Mathf.Clamp(currCooldown, 0.0f, cooldown);
        }

        if (Input.GetKey(KeyCode.Space) && shootReady)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D re = bullet.GetComponent<Rigidbody2D>();
            re.velocity = firePoint.up * bulletSpeed;
            eggCount++;
            currCooldown = 0.0f;
        }

        shootSlider.value = 1 - (currCooldown / cooldown);

        if (shootSlider.value >= 1.0f || shootSlider.value <= 0.0f)
        {
            shootSlider.gameObject.SetActive(false);
        } else
        {
            shootSlider.gameObject.SetActive(true);
        }
    }

}