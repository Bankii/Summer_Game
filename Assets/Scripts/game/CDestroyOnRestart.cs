using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDestroyOnRestart : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (CGame.inst().isRestart())
        {
            Destroy(this.gameObject);
        }
	}
}
