using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    void Update()
    {
        //Fire();
    }
    
    //public void Fire()
    //{
        //Shoot();
    //}

    public void Shoot()
    {
        SoundManager.PlaySound("Shoot");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
