using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundVolume : MonoBehaviour {

    private AudioSource _audioSrc;

    // Use this for initialization
    void Start()
    {
        _audioSrc = gameObject.GetComponent<AudioSource>();
        if (CSaveLoad.soundVolume != 0)
        {
            _audioSrc.volume = _audioSrc.volume * CSaveLoad.soundVolume;
        }
        else
            _audioSrc.volume = 0;
    }
    

}
