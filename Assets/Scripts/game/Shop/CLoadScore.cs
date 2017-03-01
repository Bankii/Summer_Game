using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLoadScore : MonoBehaviour {

    private Text _txtComp;

	// Use this for initialization
	void Start () {
        _txtComp = gameObject.GetComponent<Text>();
        _txtComp.text = CSaveLoad.bestScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
