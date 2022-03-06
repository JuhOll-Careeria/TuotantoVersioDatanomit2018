using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPieniHaamu : MonoBehaviour
{
    public int health;

    public GameObject deathEffect;

    private LevelManager lm;

    private void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;


        if (health <= 0)
        {
            SoundManager.PlaySound("PieniHaamu");
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
