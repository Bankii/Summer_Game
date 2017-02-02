using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CComboUI : MonoBehaviour {

    private Text _txtComponent;
    private Slider _sliderComponent;

	// Use this for initialization
	void Start () {
        _txtComponent = gameObject.GetComponent<Text>();
        _sliderComponent = gameObject.GetComponent<Slider>();
        if (_sliderComponent != null)
        {
            _sliderComponent.maxValue = CGame.inst()._comboMaxTime;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_txtComponent != null)
        {
            _txtComponent.text = "x" + CGame.inst().getCoinMultip();
        }
        if (_sliderComponent != null)
        {
            _sliderComponent.value = CGame.inst()._comboMaxTime - CGame.inst().getComboElapsedTime();
        }
    }
}
