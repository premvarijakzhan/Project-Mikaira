using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip GameOver;
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip Pistol;
    private AudioSource audioSource;

    //needs to be only one instance of the singleton, hence this code
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }

    //Reference to what audioSource is
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //These are the methods used to play each sound effect
    public void Sound_ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
    public void Sound_GameOver()
    {
        audioSource.PlayOneShot(GameOver);
    }
    public void Sound_PlayerJump()
    {
        audioSource.PlayOneShot(playerJump);
    }
    public void Sound_PlayerShoot_Pistol()
    {
        audioSource.PlayOneShot(Pistol);
    }
    //public void Sound_RandomBGM()
    //{
    //    int _ind = Random.Range(0, musicChoices.Length);
    //    AudioClip _bgmchoice = musicChoices[_ind];
    //    audioSource.PlayOneShot(_bgmchoice);
    //}
}
