using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPref;
    public AudioSource shootingSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot()
    {
        shootingSound.Play();
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
