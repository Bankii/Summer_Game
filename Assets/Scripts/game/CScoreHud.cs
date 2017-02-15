using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreHud : MonoBehaviour {

    private Text _text;

	// Use this for initialization
	void Start () {
        _text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = CGame.inst().getScore().ToString();
	}
}
