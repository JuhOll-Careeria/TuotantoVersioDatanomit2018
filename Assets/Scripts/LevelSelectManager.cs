using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public string[] levelTags;

    public GameObject[] locks;

    public bool[] levelUnlocked;

    private void Start()
    {
        for(int i = 0; i < levelTags.Length; i++)
        {
            if(PlayerPrefs.GetInt(levelTags[i]) == null)
            {
                levelUnlocked[i] = false;
            } 
            else if (PlayerPrefs.GetInt(levelTags[i]) == 0)
            {
                levelUnlocked[i] = false;
            }
            else
            {
                levelUnlocked[i] = true;
            }
            if (levelUnlocked[i])
            {
                locks[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        
    }
}
