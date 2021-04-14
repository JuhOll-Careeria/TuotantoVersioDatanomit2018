using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileDamageController : MonoBehaviour
{
    [SerializeField] private float trapDamage;
    [SerializeField] private MobileHealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage();
        }
    }
    void Damage()
    {
        healthController.playerHealth = healthController.playerHealth - trapDamage;
        healthController.UpdateHealth();
    }
}
