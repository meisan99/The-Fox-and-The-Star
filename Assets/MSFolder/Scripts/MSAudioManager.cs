using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSAudioManager : MonoBehaviour
{
    public AudioSource[] myMusic;
    public AudioSource[] mySFX;
    public int musicIndex;

    private void Awake()
    {
        
    }

    public void PlayMusic()
    {
        myMusic[musicIndex].Play();
    }


    public void StopMusic()
    {
        myMusic[musicIndex].Stop();
    }

    public void PauseMusic()
    {
        myMusic[musicIndex].Pause();
    }

    public void UnPauseMusic()
    {
        myMusic[musicIndex].UnPause();
    }

    public void PlaySFX(int sound)
    {
        mySFX[sound].Play();
    }

    public void PauseAllSFX()
    {
        for(int i = 0; i< mySFX.Length; i++)
        {
            mySFX[i].Pause();
        }
    }

    public void UnPauseAllSFX()
    {
        for (int i = 0; i < mySFX.Length; i++)
        {
            mySFX[i].UnPause();
        }
    }
}
