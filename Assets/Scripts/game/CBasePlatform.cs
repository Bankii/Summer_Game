using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBasePlatform : MonoBehaviour {


    private Collider2D _collider;

	void Start () {

        _collider = GetComponent<Collider2D>();

        Debug.Log(_collider);
	}
	

	void Update () {

        

        if (CGame.inst()._difficulty >= 1)
        {
            _collider.enabled = false;
        }

        if (CGame.inst().isRestart())
        {
            _collider.enabled = true;
        }	
	}
}
