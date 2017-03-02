using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundVolume : MonoBehaviour {

    private AudioSource _audioSrc;

    private float _originalVolume;
    private float _usedMultip;

    // Use this for initialization
    void Start()
    {
        _audioSrc = gameObject.GetComponent<AudioSource>();

        _originalVolume = _audioSrc.volume;

        _audioSrc.volume = _audioSrc.volume * CSaveLoad.soundVolume;

        _usedMultip = CSaveLoad.soundVolume;
    }
    
    void Update()
    {
        if (CSaveLoad.soundVolume != _usedMultip)
        {
            _audioSrc.volume = _originalVolume * CSaveLoad.soundVolume;
            _usedMultip = CSaveLoad.soundVolume;
        }
    }

}
