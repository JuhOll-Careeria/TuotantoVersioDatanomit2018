using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerHitSound, shootSound, jumpSound, enemySound, dashSound, deathSound, trambolineSound, coinSound;
    static AudioSource audioSrc;



void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("Vahinko");
        coinSound = Resources.Load<AudioClip>("Mansikka");
        trambolineSound = Resources.Load<AudioClip>("Trampoliini");
        deathSound = Resources.Load<AudioClip>("Kuolema");
        dashSound = Resources.Load<AudioClip>("Dash");
        enemySound = Resources.Load<AudioClip>("AmpiainenBzzz");
        jumpSound = Resources.Load<AudioClip>("Hyppy");
        shootSound = Resources.Load<AudioClip>("Shoot");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Vahinko"://
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "Mansikka"://
                audioSrc.PlayOneShot(coinSound);
                break;
            case "Trampoliini":
                audioSrc.PlayOneShot(trambolineSound);
                break;
            case "Kuolema"://
                audioSrc.PlayOneShot(deathSound);
                break;
            case "Dash"://
                audioSrc.PlayOneShot(dashSound);
                break;
            case "Hyppy"://
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "AmpiainenBzzz"://
                audioSrc.PlayOneShot(enemySound);
                break;

        }
    }


}
