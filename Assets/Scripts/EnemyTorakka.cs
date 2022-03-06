using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorakka : MonoBehaviour
{
    public int health;

    public GameObject deathEffect;

    private LevelManager lm;

    private void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;


        if (health <= 0)
        {
            SoundManager.PlaySound("Torakka");
            Die();
        }
    }
    void Die()
    {
        lm.score += 50;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
