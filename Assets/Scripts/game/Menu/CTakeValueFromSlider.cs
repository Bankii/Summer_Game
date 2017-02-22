using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTakeValueFromSlider : MonoBehaviour {

    private Text _textCmp;

    public Slider _slider;

	// Use this for initialization
	void Start () {
        _textCmp = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _textCmp.text = (Mathf.Ceil(_slider.value * 100)).ToString();
        
	}
}
