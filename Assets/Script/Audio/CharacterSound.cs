using System.Collections;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hero1;
    public AudioClip hero2;
    public AudioClip hero3;

    public void PlayHero1()
    {
        audioSource.PlayOneShot(hero1);
    }

    public void PlayHero2()
    {
        audioSource.PlayOneShot(hero2);
    }

    public void PlayHero3()
    {
        audioSource.PlayOneShot(hero3);
    }

}