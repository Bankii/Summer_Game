using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDust : MonoBehaviour {

    Animator _anim;

	// Use this for initialization
	void Start () {

        _anim = GetComponent<Animator>();
		
	}

    // Update is called once per frame
    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_anim.IsInTransition(0))
        {
            Destroy(this.gameObject);
        }
    }
}
