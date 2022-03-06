using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        EnemyMouse enemyMouse = hitInfo.GetComponent<EnemyMouse>();
        EnemyTorakka enemyTorakka = hitInfo.GetComponent<EnemyTorakka>();
        EnemyPieniHaamu enemyPieniHaamu = hitInfo.GetComponent<EnemyPieniHaamu>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (enemyMouse != null)
        {
            enemyMouse.TakeDamage(damage);
        }
        if (enemyTorakka != null)
        {
            enemyTorakka.TakeDamage(damage);
        }
        if (enemyPieniHaamu != null)
        {
            enemyPieniHaamu.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
