using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] soundarray;

    private void Awake()
    {
        foreach(Sound indivsound in soundarray)
        {
           indivsound.source = gameObject.AddComponent<AudioSource>();
            indivsound.source.clip = indivsound.clip;

            indivsound.source.volume = indivsound.volume;
            indivsound.source.pitch = indivsound.pitch;
        }
    }

    public void Player(string soundname)
    {
       Sound findclip = Array.Find(soundarray, sound => sound.name == soundname);
        findclip.source.Play();
    }
}
