using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    public GameObject Player;
    public Transform respawnPoint;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Respawn()
    {
        MobileHealthController.Instance.Health();


        Player.transform.position = respawnPoint.position;
    }
}
