using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine.Component;

public class SoundControl : MonoBehaviour
{
    public AudioClip ambient;
    public Texture mute;
    public Texture unmute;

    public void Play()
    {
        GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void Pause()
    {
        GetComponent<AudioSource>().Pause();
    }

    public void ToggleMute()
    {
        if(GetComponent<AudioSource>().mute){
            GetComponent<AudioSource>().mute=false;
        } else {
            GetComponent<AudioSource>().mute=true;
        }
    }

    //public bool IsMute(){
        //return audio.mute;
    //}
}
