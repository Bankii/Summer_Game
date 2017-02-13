using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CComboUI : CText {

    //private Text _txtComponent;
    private Slider _sliderComponent;
    private int _coinMultip;

	// Use this for initialization
	void Start () {

        //_txtComponent = gameObject.GetComponent<Text>();
        _sliderComponent = gameObject.GetComponent<Slider>();

        _coinMultip = CGame.inst().getCoinMultip();

        if (_sliderComponent != null)
        {
            _sliderComponent.maxValue = CGame.inst()._comboMaxTime;
        }
    }
	
	// Update is called once per frame
	void Update () {
        base.apiUpdate();

        if (_text != null)
        {
            if (_coinMultip != CGame.inst().getCoinMultip())
            {
                _text.text = "x" + CGame.inst().getCoinMultip();
                _coinMultip = CGame.inst().getCoinMultip();
                sizeBounce(150, 5, 10);
            }
            
        }
        if (_sliderComponent != null)
        {
            _sliderComponent.value = CGame.inst()._comboMaxTime - CGame.inst().getComboElapsedTime();
        }
    }
}
