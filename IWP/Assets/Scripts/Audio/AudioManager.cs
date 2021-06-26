using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] soundarray;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach(Sound indivsound in soundarray)
        {
           indivsound.source = gameObject.AddComponent<AudioSource>();
            indivsound.source.clip = indivsound.clip;

            indivsound.source.volume = indivsound.volume;
            indivsound.source.pitch = indivsound.pitch;
            indivsound.source.loop = indivsound.loop;
        }
    }

    private void Start()
    {
       
    }

    public void Player(string soundname)
    {
       Sound findclip = Array.Find(soundarray, sound => sound.name == soundname);
        if(findclip == null)
        {
            Debug.Log("error in sound detected");
            return;
            
        }
        findclip.source.Play();
    }

   

    public void adjustVolume(float vol)
    {
        foreach (Sound indivsound in soundarray)
        {
            
            indivsound.source.volume = vol;
            
        }
    }
}
