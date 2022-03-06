using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Obsolete]

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance = null;

    public bool playerInZone;

    public string levelToLoad;

    public string mainMenu;

    public string levelTag;

    public GameObject EnterBtn;

    public GameObject Player;

    public Animator animator;


    private void Awake()
    {
     if (Instance == null)
       Instance = this;

    //else if (Instance != this)
    //   Destroy(gameObject);

      }

    private void Start()
    {
        playerInZone = false;
        EnterBtn = GameObject.Find("EnterBtn");
    }

    private void Update()
    {
        if(playerInZone == true)
        {
            animator.SetBool("isOpen", true);
        }

        else
        {
            animator.SetBool("isOpen", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            playerInZone = true;

            Enter();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            playerInZone = false;

            Exit();
        }
    }

    public void Enter()
    {
        if (playerInZone == true)
        {
            Vector3 theScale = transform.localScale;

            theScale.x = 1;
            theScale.y = 1;

            EnterBtn.transform.localScale = theScale;
        }

        //else{
           // Vector3 theScale = transform.localScale;

            //theScale.x = 0;

          //  EnterBtn.transform.localScale = theScale;
        //}
    }
    public void Exit()
    {
        if(playerInZone == false)
        {
            Vector3 theScale = transform.localScale;

            theScale.x = 0;

            EnterBtn.transform.localScale = theScale;

        }
    }

    public void LevelChange()
    {
        if (playerInZone)
        {
            PlayerPrefs.SetInt(levelTag, 1);
            Application.LoadLevel(levelToLoad);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(Player);
        }
    }

    public void LevelSwap()
    {
        Application.LoadLevel(levelToLoad);
    }

    public void MainMenu()
    {
        Application.LoadLevel(mainMenu);
    }

}
