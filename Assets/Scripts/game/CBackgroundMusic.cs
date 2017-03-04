using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBackgroundMusic : MonoBehaviour {

    private AudioSource _as;
	// Use this for initialization
	void Start () {

        _as = GetComponent<AudioSource>();

        if (!_as.isPlaying)
        {
            _as.Play();
        }
	}
	
}
