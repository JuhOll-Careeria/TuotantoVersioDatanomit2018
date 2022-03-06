using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class MobileHealthController : MonoBehaviour
{
    public static MobileHealthController Instance = null;

    public float maxHealth;
    public float currentHealth;

    public Animator animator;
    IEnumerator DeathWaitCoroutine;

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
    }

    private void Update()
    {
        if (currentHealth > 3)
        {
            currentHealth = 3;
            UpdateHealth();
        }
    }

    public void Health()
    {
        currentHealth = maxHealth;

        UpdateHealth();
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
        animator.SetBool("IsDying", true);
        SoundManager.PlaySound("Kuolema");
        StartCoroutine(DeathWait());
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsDying", false);
        LevelManager.Instance.Respawn();
    }

}
