using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Non-Diegetic sfx
    public AudioClip menuClick;
    //Non-Diegetic Music 
    public AudioClip menuMusic;
    public AudioClip placementMusic;
    public AudioClip defendMusic;

    //audio sources
    private AudioSource menuClickSource = new AudioSource();
    private AudioSource menuMusicSource = new AudioSource();
    private AudioSource placementMusicSource = new AudioSource();
    private AudioSource defendMusicSource = new AudioSource();


    private void Start()
    {

        menuClickSource.clip = menuClick;
        menuMusicSource.clip = menuMusic;
        placementMusicSource.clip = placementMusic;
        defendMusicSource.clip = defendMusic;
        

        PlayMenuMusic();
    }


    public void PlayMenuMusic()
    {
        StopAllMusic();
        menuMusicSource.Play();
        menuMusicSource.loop = true;
    }

    public void PlayDefendMusic()
    {
        StopAllMusic();
        defendMusicSource.Play();
        defendMusicSource.loop = true;
    }


    public void PlayPlacementMusic()
    {
        StopAllMusic();
        placementMusicSource.Play();
        placementMusicSource.loop = true;
    }

    //Stop Helper
    public void StopAllMusic()
    {
        menuMusicSource.Stop();
        placementMusicSource.Stop();
        defendMusicSource.Stop();
    }


}
