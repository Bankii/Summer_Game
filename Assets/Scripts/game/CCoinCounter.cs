using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCoinCounter : MonoBehaviour {

    private Text _textComp;

	// Use this for initialization
	void Start () {
        _textComp = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _textComp.text = CSaveLoad.money.ToString();
	}
}
