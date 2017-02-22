using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMusicVolume : MonoBehaviour {

    private AudioSource _audioSrc;

	// Use this for initialization
	void Start () {
        _audioSrc = gameObject.GetComponent<AudioSource>();
        if (CSaveLoad.musicVolume != 0)
        {
            _audioSrc.volume = _audioSrc.volume * CSaveLoad.musicVolume;
        }
        else
            _audioSrc.volume = 0;
	}
	
}
