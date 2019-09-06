using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Buttons : MonoBehaviour
{
    public enum ButtonTypes{Play, Pause, Stop, Mute}

    public ButtonTypes type;
    public Texture mute;
    public Texture unmute;

    private SoundControl sound;

    void Start()
    {
        sound=Camera.main.GetComponent<SoundControl>();
    }

    void OnMouseDown()
	{
        if(sound==null) return;
        switch (type)
        {
            case ButtonTypes.Play:
                sound.Play();
                break;
            case ButtonTypes.Pause:
                sound.Pause();
                break;
            case ButtonTypes.Stop:
                sound.Stop();
                break;
            case ButtonTypes.Mute:
                sound.ToggleMute(); 
			          break;
        }
    }
}
