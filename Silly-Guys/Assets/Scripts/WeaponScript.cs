using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject player;
    public Transform firePoint;
    public GameObject bulletPref;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !player.GetComponent<PlayerController>().isLocked) {
            Shoot();
        }
    }

    void Shoot()
    {
        AudioController.Instance.PlaySfx("Shoot");
        //shoot logic
        if (transform.localScale.x < 0)
        {
            Instantiate(bulletPref, firePoint.position, Quaternion.Euler(0,0, 180));
        }
        else
        {
            Instantiate(bulletPref, firePoint.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
