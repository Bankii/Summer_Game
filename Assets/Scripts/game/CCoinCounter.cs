using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCoinCounter : CText {

    //private Text _textComp;
    private int _coins;


	// Use this for initialization
	void Start () {
        //_textComp = GetComponent<Text>();
        _coins = CSaveLoad.money;
        _text.text = CSaveLoad.money.ToString();
    }

    // Update is called once per frame
    void Update () {

        base.apiUpdate();

        if (_coins != CSaveLoad.money)
        {
            _coins = CSaveLoad.money;            
            _text.text = CSaveLoad.money.ToString();
            sizeBounce(120, 4, 8);
        }
        

    }
}
