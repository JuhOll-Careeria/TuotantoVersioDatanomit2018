using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileHealthController : MonoBehaviour
{
    public static MobileHealthController Instance = null;

    public float maxHealth;
    public float currentHealth;

    [SerializeField] private Text healthText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        Health();

        UpdateHealth();
    }
    public void Health()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth()
    {
        healthText.text = currentHealth.ToString("0");
        if (currentHealth <= 0)
        {
            Kill();
            
        }
    }
    private void Kill()
    {
        LevelManager.Instance.Respawn();
        
    }
}
