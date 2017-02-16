using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreHud : CText {

    private int _score;

	// Use this for initialization
	void Start () {
        _score = CGame.inst().getScore();
	}
	
	// Update is called once per frame
	void Update () {

        base.apiUpdate();

        if (_score != CGame.inst().getScore())
        {
            _score = CGame.inst().getScore();
            _text.text = CGame.inst().getScore().ToString();
            sizeBounce(120, 4, 8);            
        }
        
	}
}
