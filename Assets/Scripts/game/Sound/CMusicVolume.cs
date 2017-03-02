using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMusicVolume : MonoBehaviour {

    private AudioSource _audioSrc;

    private float _originalVolume;
    private float _usedMultip;

    // Use this for initialization
    void Start()
    {
        _audioSrc = gameObject.GetComponent<AudioSource>();

        _originalVolume = _audioSrc.volume;

        _audioSrc.volume = _audioSrc.volume * CSaveLoad.musicVolume;

        _usedMultip = CSaveLoad.musicVolume;
    }

    void Update()
    {
        if (CSaveLoad.musicVolume != _usedMultip)
        {
            _audioSrc.volume = _originalVolume * CSaveLoad.musicVolume;
            _usedMultip = CSaveLoad.musicVolume;
        }
    }

}
