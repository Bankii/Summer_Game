using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLoadValueToSlider : MonoBehaviour {

    public bool _isSound;
    private Slider _slider;

	// Use this for initialization
	void Start () {
        _slider = gameObject.GetComponent<Slider>();
        if (_isSound)
        {
            _slider.value = CSaveLoad.soundVolume;
        }
        else
        {
            _slider.value = CSaveLoad.musicVolume;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
